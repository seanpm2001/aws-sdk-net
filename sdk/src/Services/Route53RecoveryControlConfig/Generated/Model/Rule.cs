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
 * Do not modify this file. This file is generated from the route53-recovery-control-config-2020-11-02.normal.json service model.
 */
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Net;

using Amazon.Runtime;
using Amazon.Runtime.Internal;

namespace Amazon.Route53RecoveryControlConfig.Model
{
    /// <summary>
    /// A safety rule. A safety rule can be an assertion rule or a gating rule.
    /// </summary>
    public partial class Rule
    {
        private AssertionRule _assertion;
        private GatingRule _gating;

        /// <summary>
        /// Gets and sets the property ASSERTION. 
        /// <para>
        /// An assertion rule enforces that, when a routing control state is changed, the criteria
        /// set by the rule configuration is met. Otherwise, the change to the routing control
        /// is not accepted.
        /// </para>
        /// </summary>
        public AssertionRule ASSERTION
        {
            get { return this._assertion; }
            set { this._assertion = value; }
        }

        // Check to see if ASSERTION property is set
        internal bool IsSetASSERTION()
        {
            return this._assertion != null;
        }

        /// <summary>
        /// Gets and sets the property GATING. 
        /// <para>
        /// A gating rule verifies that a set of gating controls evaluates as true, based on a
        /// rule configuration that you specify. If the gating rule evaluates to true, Amazon
        /// Route 53 Application Recovery Controller allows a set of routing control state changes
        /// to run and complete against the set of target controls.
        /// </para>
        /// </summary>
        public GatingRule GATING
        {
            get { return this._gating; }
            set { this._gating = value; }
        }

        // Check to see if GATING property is set
        internal bool IsSetGATING()
        {
            return this._gating != null;
        }

    }
}