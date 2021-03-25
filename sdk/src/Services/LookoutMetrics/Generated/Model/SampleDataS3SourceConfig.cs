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
 * Do not modify this file. This file is generated from the lookoutmetrics-2017-07-25.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Net;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.LookoutMetrics.Model
{
    /// <summary>
    /// Contains information about the source configuration in Amazon S3.
    /// </summary>
    public partial class SampleDataS3SourceConfig
    {
        private FileFormatDescriptor _fileFormatDescriptor;
        private List<string> _historicalDataPathList = new List<string>();
        private string _roleArn;
        private List<string> _templatedPathList = new List<string>();

        /// <summary>
        /// Gets and sets the property FileFormatDescriptor.
        /// </summary>
        [AWSProperty(Required=true)]
        public FileFormatDescriptor FileFormatDescriptor
        {
            get { return this._fileFormatDescriptor; }
            set { this._fileFormatDescriptor = value; }
        }

        // Check to see if FileFormatDescriptor property is set
        internal bool IsSetFileFormatDescriptor()
        {
            return this._fileFormatDescriptor != null;
        }

        /// <summary>
        /// Gets and sets the property HistoricalDataPathList. 
        /// <para>
        /// An array of strings containing the historical set of data paths.
        /// </para>
        /// </summary>
        [AWSProperty(Min=1, Max=1)]
        public List<string> HistoricalDataPathList
        {
            get { return this._historicalDataPathList; }
            set { this._historicalDataPathList = value; }
        }

        // Check to see if HistoricalDataPathList property is set
        internal bool IsSetHistoricalDataPathList()
        {
            return this._historicalDataPathList != null && this._historicalDataPathList.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property RoleArn. 
        /// <para>
        /// The Amazon Resource Name (ARN) of the role.
        /// </para>
        /// </summary>
        [AWSProperty(Required=true, Max=256)]
        public string RoleArn
        {
            get { return this._roleArn; }
            set { this._roleArn = value; }
        }

        // Check to see if RoleArn property is set
        internal bool IsSetRoleArn()
        {
            return this._roleArn != null;
        }

        /// <summary>
        /// Gets and sets the property TemplatedPathList. 
        /// <para>
        /// An array of strings containing the list of templated paths.
        /// </para>
        /// </summary>
        [AWSProperty(Min=1, Max=1)]
        public List<string> TemplatedPathList
        {
            get { return this._templatedPathList; }
            set { this._templatedPathList = value; }
        }

        // Check to see if TemplatedPathList property is set
        internal bool IsSetTemplatedPathList()
        {
            return this._templatedPathList != null && this._templatedPathList.Count > 0; 
        }

    }
}