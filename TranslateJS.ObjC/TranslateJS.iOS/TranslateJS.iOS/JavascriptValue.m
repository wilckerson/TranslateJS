//
//  JsValue.m
//  TranslateJS.iOS
//
//  Created by Wilckerson Ganda on 17/01/15.
//  Copyright (c) 2015 TranslateJS. All rights reserved.
//

#import "JavascriptValue.h"

@implementation JavascriptValue

NSDictionary* properties;

-(id) initWithDictionary: (NSDictionary*)dict{
    
    properties = dict;
    return [super init];
}

-(JavascriptValue*)getProp: (NSString*)propertyName{
    return [properties objectForKey:propertyName ];
}

-(void)setProp:(NSString*)propName withValue: (JavascriptValue*) propValue{
    [properties setValue:propValue forKey:propName];
}

@end