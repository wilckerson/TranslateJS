using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Jint;
using System.Diagnostics;
using TranslateJS.Core;

namespace TranslateJS.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            
            RunJint2();

            timer.Stop();
            Console.WriteLine("Jint {0}ms", timer.ElapsedMilliseconds);

            timer.Restart();

            RunNativeJS2();

            timer.Stop();
            Console.WriteLine("NativeJs {0}ms", timer.ElapsedMilliseconds);

           
            Console.ReadKey();
        }
        static Engine jintEngine = new Engine();
        public static void RunJint()
        {
            
            jintEngine.Execute(@"
               var myMod = {};
                (function(exports){
                   exports.randomElt = function(arr){
                        return arr[Math.floor(arr.length * Math.random())];
                    };
                })(myMod);
                myMod.randomElt([1,2,3,4,5]);
            ");
            var result = jintEngine.GetCompletionValue();
        }

        public static void RunJint2()
        {
            jintEngine.Execute(@"
               var originalObj = {prop1: 'hue', prop2: 123};
                var arr = [];
                for(var i = 0; i < 100000; i++)
                {
                    var copyObj = {};
                    copyObj.prop1 = originalObj.prop1;
                    copyObj.prop2 = originalObj.prop2;
                    arr.push(copyObj);
                }

                arr;
            ");
            var result = jintEngine.GetCompletionValue();
        }

        public static void RunNativeJS2(){

            var originalObj = new JsValue() { {"prop1","hue"}, {"prop2",123} };
            var arr = new JsValue();

            //for(int i = 0 ... Roda 2x mais rapido
            for (JsValue i = 0; i < 100000; i++)
            {
                var copyObj = new JsValue();
                copyObj["prop1"] = originalObj["prop1"];
                copyObj["prop2"] = originalObj["prop2"];
                arr.Add(copyObj);
            }

        }

        public static void RunNativeJS()
        {
            var myMod = new JsValue();

            JsFunc anonymous_function1 = (function1_arguments) =>
            {
                var exports = function1_arguments[0];
                JsFunc anonymous_function2 = (function2_arguments) =>
                {
                    var arr = function2_arguments[0];
                    return arr[(int)JsMath.floor(arr.Length * JsMath.random())];
                };
                exports["randomElt"] = anonymous_function2;
                return null;
            };
            anonymous_function1(myMod);

            var result = myMod["randomElt"].Execute(new JsValue { 1, 2, 3, 4, 5 });
        }
    }
}
