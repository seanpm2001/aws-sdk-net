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
    /// This is the response object from the ListMetricSets operation.
    /// </summary>
    public partial class ListMetricSetsResponse : AmazonWebServiceResponse
    {
        private List<MetricSetSummary> _metricSetSummaryList = new List<MetricSetSummary>();
        private string _nextToken;

        /// <summary>
        /// Gets and sets the property MetricSetSummaryList. 
        /// <para>
        /// A list of the datasets in the AWS Region, with configuration details for each.
        /// </para>
        /// </summary>
        public List<MetricSetSummary> MetricSetSummaryList
        {
            get { return this._metricSetSummaryList; }
            set { this._metricSetSummaryList = value; }
        }

        // Check to see if MetricSetSummaryList property is set
        internal bool IsSetMetricSetSummaryList()
        {
            return this._metricSetSummaryList != null && this._metricSetSummaryList.Count > 0; 
        }

        /// <summary>
        /// Gets and sets the property NextToken. 
        /// <para>
        /// If the response is truncated, the list call returns this token. To retrieve the next
        /// set of results, use the token in the next list request. 
        /// </para>
        /// </summary>
        [AWSProperty(Min=1, Max=3000)]
        public string NextToken
        {
            get { return this._nextToken; }
            set { this._nextToken = value; }
        }

        // Check to see if NextToken property is set
        internal bool IsSetNextToken()
        {
            return this._nextToken != null;
        }

    }
}