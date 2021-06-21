/*
 * Copyright Amazon.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  http://aws.amazon.com/apache2.0
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */

/*
 * Do not modify this file. This file is generated from the cloudformation-2010-05-15.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

using Amazon.CloudFormation.Model;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.Runtime.Internal.Transform;
using Amazon.Runtime.Internal.Util;
namespace Amazon.CloudFormation.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Response Unmarshaller for BatchDescribeTypeConfigurationsError Object
    /// </summary>  
    public class BatchDescribeTypeConfigurationsErrorUnmarshaller : IUnmarshaller<BatchDescribeTypeConfigurationsError, XmlUnmarshallerContext>, IUnmarshaller<BatchDescribeTypeConfigurationsError, JsonUnmarshallerContext>
    {
        /// <summary>
        /// Unmarshaller the response from the service to the response class.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        public BatchDescribeTypeConfigurationsError Unmarshall(XmlUnmarshallerContext context)
        {
            BatchDescribeTypeConfigurationsError unmarshalledObject = new BatchDescribeTypeConfigurationsError();
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.ReadAtDepth(originalDepth))
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("ErrorCode", targetDepth))
                    {
                        var unmarshaller = StringUnmarshaller.Instance;
                        unmarshalledObject.ErrorCode = unmarshaller.Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("ErrorMessage", targetDepth))
                    {
                        var unmarshaller = StringUnmarshaller.Instance;
                        unmarshalledObject.ErrorMessage = unmarshaller.Unmarshall(context);
                        continue;
                    }
                    if (context.TestExpression("TypeConfigurationIdentifier", targetDepth))
                    {
                        var unmarshaller = TypeConfigurationIdentifierUnmarshaller.Instance;
                        unmarshalledObject.TypeConfigurationIdentifier = unmarshaller.Unmarshall(context);
                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return unmarshalledObject;
                }
            }

            return unmarshalledObject;
        }

        /// <summary>
        /// Unmarshaller error response to exception.
        /// </summary>  
        /// <param name="context"></param>
        /// <returns></returns>
        public BatchDescribeTypeConfigurationsError Unmarshall(JsonUnmarshallerContext context)
        {
            return null;
        }


        private static BatchDescribeTypeConfigurationsErrorUnmarshaller _instance = new BatchDescribeTypeConfigurationsErrorUnmarshaller();        

        /// <summary>
        /// Gets the singleton.
        /// </summary>  
        public static BatchDescribeTypeConfigurationsErrorUnmarshaller Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}