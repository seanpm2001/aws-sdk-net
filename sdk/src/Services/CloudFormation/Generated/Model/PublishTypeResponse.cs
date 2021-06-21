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
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Net;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.CloudFormation.Model
{
    /// <summary>
    /// This is the response object from the PublishType operation.
    /// </summary>
    public partial class PublishTypeResponse : AmazonWebServiceResponse
    {
        private string _publicTypeArn;

        /// <summary>
        /// Gets and sets the property PublicTypeArn. 
        /// <para>
        /// The Amazon Resource Number (ARN) assigned to the public extension upon publication.
        /// </para>
        /// </summary>
        [AWSProperty(Max=1024)]
        public string PublicTypeArn
        {
            get { return this._publicTypeArn; }
            set { this._publicTypeArn = value; }
        }

        // Check to see if PublicTypeArn property is set
        internal bool IsSetPublicTypeArn()
        {
            return this._publicTypeArn != null;
        }

    }
}