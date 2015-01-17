//
//  ViewController.m
//  TranslateJS.iOS
//
//  Created by Wilckerson Ganda on 17/01/15.
//  Copyright (c) 2015 TranslateJS. All rights reserved.
//

#import "ViewController.h"
#import <JavaScriptCore/JavaScriptCore.h>
#import "JavascriptValue.h"

@interface ViewController ()

@end

@implementation ViewController

JSContext* context ;

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
    
    context = [[JSContext alloc] init];
    
    NSTimeInterval start = [NSDate timeIntervalSinceReferenceDate];
    
    [self runJSCore2];
    
    NSTimeInterval duration = [NSDate timeIntervalSinceReferenceDate] - start;
    
    NSLog(@"%f",duration * 1000);
    
}

-(void)runJSCore{
   
    JSValue* result = [context evaluateScript:
                       @"var myMod = {};(function(exports){exports.randomElt = function(arr){return arr[Math.floor(arr.length * Math.random())];};})(myMod);myMod.randomElt([1,2,3,4,5]);"];
}

-(void)runJSCore2{
    
    JSValue* result = [context evaluateScript:
                       @"var originalObj = {prop1: 'hue', prop2: 123};var arr = [];for(var i = 0; i < 100000; i++){var copyObj = {};copyObj.prop1 = originalObj.prop1;copyObj.prop2 = originalObj.prop2;arr.push(copyObj);}arr;"];
}

-(void)runNativeJS{
    
    JavascriptValue* originalObj = [[JavascriptValue alloc] initWithDictionary:@{
                                                                                 @"prop": @"hue",
                                                                                 @"prop2": @123
                                                                                 }];
    
    JavascriptValue* arr = [[JavascriptValue alloc] init];
    
    //for(int i = 0 ... Roda 2x mais rapido
//    for (int i = 0; i < 100000; i++)
//    {
//        JavascriptValue* copyObj = [[JavascriptValue alloc] init];
//        [copyObj setProp:@"prop1" withValue: [originalObj getProp:@"prop1"]];
//        [copyObj setProp:@"prop1" withValue: [originalObj getProp:@"prop2"]];
//        arr setValue:<#(id)#> forKey:<#(NSString *)#> in .Add(copyObj);
//         */
//    }
    
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

@end
