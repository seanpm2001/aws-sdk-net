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
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;

namespace Amazon.DynamoDBv2.DocumentModel
{
    /// <summary>
    /// Builder that constructs a <see cref="Table"/>
    /// </summary>
    public class TableBuilder : ITableBuilder
    {
        private Table _table;

        /// <summary>
        /// Creates a builder object to construct a <see cref="Table"/>
        /// </summary>
        /// <param name="ddbClient">Client to use to access DynamoDB.</param>
        /// <param name="tableName">Table name</param>
        public TableBuilder(IAmazonDynamoDB ddbClient, string tableName) :
             this(ddbClient, tableName, DynamoDBEntryConversion.CurrentConversion, false)
        {
        }

        /// <summary>
        /// Creates a builder object to construct a <see cref="Table"/>
        /// </summary>
        /// <param name="ddbClient">Client to use to access DynamoDB.</param>
        /// <param name="tableName">Table name</param>
        /// <param name="conversion">Conversion to use for converting .NET values to DynamoDB values.</param>
        /// <param name="isEmptyStringValueEnabled">If the property is false, empty string values will be interpreted as null values.</param>
        public TableBuilder(IAmazonDynamoDB ddbClient, string tableName, DynamoDBEntryConversion conversion, bool isEmptyStringValueEnabled)
        {
            var config = new TableConfig(tableName, conversion, Table.DynamoDBConsumer.DocumentModel, null, isEmptyStringValueEnabled);

            _table = new Table(ddbClient, config);
            _table.ClearTableData();
        }

        /// <summary>
        /// Creates a builder object to construct a <see cref="Table"/>
        /// </summary>
        /// <param name="ddbClient">Client to use to access DynamoDB.</param>
        /// <param name="config">Configuration to use for the table.</param>
        public TableBuilder(IAmazonDynamoDB ddbClient, TableConfig config)
        {
            _table = new Table(ddbClient, config);
            _table.ClearTableData();
        }

        /// <inheritdoc/>
        public Table Build()
        {
            return _table;
        }

        /// <inheritdoc/>
        public ITableBuilder AddHashKey(string hashKeyAttribute, DynamoDBEntryType type)
        {
            // TODO - validate that only one was added
            // TODO - validate inputs

            _table.HashKeys.Add(hashKeyAttribute);

            _table.Keys.Add(hashKeyAttribute, new KeyDescription
            {
                IsHash = true,
                Type = type
            });

            return this;
        }

        /// <inheritdoc/>
        public ITableBuilder AddRangeKey(string rangeKeyAttribute, DynamoDBEntryType type)
        {
            // TODO - validate that only one was added
            // TODO - validate inputs

            _table.RangeKeys.Add(rangeKeyAttribute);

            _table.Keys.Add(rangeKeyAttribute, new KeyDescription
            {
                IsHash = false,
                Type = type
            });

            return this;
        }

        /// <inheritdoc/>
        public ITableBuilder AddLocalSecondaryIndex(string indexName, string rangeKeyAttribute, DynamoDBEntryType type)
        {
            // TODO - validate that the hash key has been added in advance
            // TODO - validate that only one was added
            // TODO - validate inputs
            // TODO - validate range key isn't already a key
            // TODO - where does type go?

            _table.LocalSecondaryIndexNames.Add(indexName);

            _table.LocalSecondaryIndexes.Add(indexName, new LocalSecondaryIndexDescription
            {
                IndexName = indexName,
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement { AttributeName = _table.HashKeys[0], KeyType = KeyType.HASH },
                    new KeySchemaElement { AttributeName = rangeKeyAttribute, KeyType = KeyType.RANGE }
                }
            });

            return this;
        }

        /// <inheritdoc/>
        public ITableBuilder AddGlobalSecondaryIndex(string indexName, string hashkeyAttribute, DynamoDBEntryType hashKeyType, string rangeKeyAttribute, DynamoDBEntryType rangeKeyType)
        {
            // TODO - validate inputs
            // TODO - where does type go?

            _table.GlobalSecondaryIndexNames.Add(indexName);

            _table.GlobalSecondaryIndexes.Add(indexName, new GlobalSecondaryIndexDescription
            {
                IndexName = indexName,
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement { AttributeName = hashkeyAttribute, KeyType = KeyType.HASH },
                    new KeySchemaElement { AttributeName = rangeKeyAttribute, KeyType = KeyType.RANGE }
                }
            });

            return this;
        }
    }
}
