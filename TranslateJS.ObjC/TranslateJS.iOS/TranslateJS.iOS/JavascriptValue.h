//
//  JsValue.h
//  TranslateJS.iOS
//
//  Created by Wilckerson Ganda on 17/01/15.
//  Copyright (c) 2015 TranslateJS. All rights reserved.
//
#import <Foundation/Foundation.h>

@interface JavascriptValue : NSObject

-(id) initWithDictionary: (NSMutableDictionary*)dict;
-(JavascriptValue*)getProp: (NSString*)propertyName;
-(void)setProp:(NSString*)propName withValue: (JavascriptValue*) propValue

@end

