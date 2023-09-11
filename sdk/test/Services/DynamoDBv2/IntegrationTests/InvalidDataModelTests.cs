using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AWSSDK_DotNet.IntegrationTests.Tests.DynamoDB
{
    public partial class DynamoDBTests : TestBase<AmazonDynamoDBClient>
    {
        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_MissingHashKey()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithMissingHashKey
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "Big River",
                Score = 120,
                ManagerName = "Barbara",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithMissingHashKey>();
            Assert.AreEqual(0, table.HashKeys.Count);

            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Load(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Save(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Delete(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => context.Query<EmployeeWithMissingHashKey>("Alan")); // Message: Index was out of range. Must be non-negative and less than the size of the collection. ComposeQueryFilter
            
            var emps = context.Scan<EmployeeWithMissingHashKey>();
            Assert.AreEqual(2, emps.ToList().Count);
        }

        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_InvalidHashKey()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithInvalidHashKey
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "Big River",
                Score = 120,
                ManagerName = "Barbara",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithInvalidHashKey>();
            Assert.AreEqual(1, table.HashKeys.Count);

            Assert.AreEqual("MiddleName", table.HashKeys.First());
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Load(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Save(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Delete(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Query<EmployeeWithInvalidHashKey>("Alan").ToList()); // Message: Query condition missed key schema element: Name

            var emps = context.Scan<EmployeeWithInvalidHashKey>();
            Assert.AreEqual(2, emps.ToList().Count);
        }

        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_MissingRangeKey()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithMissingRangeKey
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "Big River",
                Score = 120,
                ManagerName = "Barbara",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithMissingRangeKey>();
            Assert.AreEqual(0, table.RangeKeys.Count);

            // Load, Save and Delete
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Load(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Save(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Delete(employee)); // Message: The provided key element does not match the schema

            // Query based on hash key + range key
            // Exception Message - Value cannot be null. Parameter name: key
            Assert.ThrowsException<ArgumentNullException>(() => context.Query<EmployeeWithMissingRangeKey>("Alan", QueryOperator.GreaterThan, 5).ToList());

            // Scan items with Age > 5
            var emps = context.Scan<EmployeeWithMissingRangeKey>(new ScanCondition("Age", ScanOperator.GreaterThan, 5)).ToList();
            Assert.AreEqual(2, emps.Count);

            // Scan all rows
            emps = context.Scan<EmployeeWithMissingRangeKey>().ToList();
            Assert.AreEqual(2, emps.Count);
        }

        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_InvalidRangeKey()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithInvalidRangeKey
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "Big River",
                Score = 120,
                ManagerName = "Barbara",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithInvalidRangeKey>();
            Assert.AreEqual(1, table.RangeKeys.Count);
            Assert.AreEqual("MiddleName", table.RangeKeys.First());

            // Load, Save and Delete
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Load(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Save(employee)); // Message: The provided key element does not match the schema
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Delete(employee)); // Message: The provided key element does not match the schema

            // Query based on hash key + range key
            // Exception Message - Query condition missed key schema element: Age
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Query<EmployeeWithMissingRangeKey>("Alan", QueryOperator.Equal, "John").ToList());

            // Scan items with Age > 5
            var emps = context.Scan<EmployeeWithMissingRangeKey>(new ScanCondition("Age", ScanOperator.GreaterThan, 5)).ToList();
            Assert.AreEqual(2, emps.Count);

            // Scan all rows
            emps = context.Scan<EmployeeWithMissingRangeKey>().ToList();
            Assert.AreEqual(2, emps.Count);
        }

        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_MissingLSI()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithMissingLSI
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "Big River",
                Score = 120,
                ManagerName = "Barbara",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithMissingLSI>();
            Assert.AreEqual(0, table.LocalSecondaryIndexes.Count);

            context.Save(employee);
            var emp = context.Load(employee);
            Assert.AreEqual("David", emp.Name);

            // Query local index for items with Hash-Key = "David"
            // Exception message - Unable to locate index [LocalIndex] on table [DotNetTests-HashRangeTable]
            Assert.ThrowsException<InvalidOperationException>(() => context.Query<EmployeeWithMissingLSI>("David", new DynamoDBOperationConfig { IndexName = "LocalIndex" }).ToList());

            // Scan local index for items with Name = "David"
            var emps = context.Scan<EmployeeWithMissingLSI>(
                new List<ScanCondition> { new ScanCondition("Name", ScanOperator.Equal, "David") },
                new DynamoDBOperationConfig { IndexName = "LocalIndex" }).ToList();
            Assert.AreEqual(1, emps.Count);
            Assert.AreEqual("David", emps[0].Name);
            Assert.AreEqual("Barbara", emps[0].ManagerName);

            // Scan all rows
            emps = context.Scan<EmployeeWithMissingLSI>().ToList();
            Assert.AreEqual(3, emps.Count);

            context.Delete(employee);
        }

        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_InvalidLSI()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithInvalidLSI
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "Big River",
                Score = 120,
                ManagerName = "Barbara",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithInvalidLSI>();
            Assert.AreEqual(1, table.LocalSecondaryIndexes.Count);
            var lsi = table.LocalSecondaryIndexes["LocalIndex"];
            Assert.AreEqual("LocalIndex", lsi.IndexName);
            Assert.AreEqual(2, lsi.KeySchema.Count);
            Assert.AreEqual("Name", lsi.KeySchema[0].AttributeName);
            Assert.AreEqual(KeyType.HASH, lsi.KeySchema[0].KeyType);
            Assert.AreEqual("MiddleName", lsi.KeySchema[1].AttributeName);
            Assert.AreEqual(KeyType.RANGE, lsi.KeySchema[1].KeyType);

            context.Save(employee);
            var emp = context.Load(employee);
            Assert.AreEqual("David", emp.Name);

            // Query local index for items with Hash-Key = "David"
            var emps = context.Query<EmployeeWithInvalidLSI>(
                "David", 
                new DynamoDBOperationConfig { IndexName = "LocalIndex" })
                .ToList();
            Assert.AreEqual(1, emps.Count);
            Assert.AreEqual("David", emps[0].Name);
            Assert.AreEqual("Barbara", emps[0].ManagerName);

            // Query local index for items with Hash-Key = "David" and LSI Range key > "Adam"
            //Exception Message - Query condition missed key schema element: Manager
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Query<EmployeeWithInvalidLSI>(
                "David",
                QueryOperator.GreaterThan,
                new List<string> { "Adam" },
                new DynamoDBOperationConfig { IndexName = "LocalIndex" })
                .ToList());

            // Scan local index for items with MiddleName = "Jacob"
            emps = context.Scan<EmployeeWithInvalidLSI>(
                new List<ScanCondition> { new ScanCondition("MiddleName", ScanOperator.Equal, "Jacob") },
                new DynamoDBOperationConfig { IndexName = "LocalIndex" }).ToList();
            Assert.AreEqual(1, emps.Count);

            // Scan all rows
            emps = context.Scan<EmployeeWithInvalidLSI>().ToList();
            Assert.AreEqual(3, emps.Count);

            context.Delete(employee);
        }

        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_MissingGSI()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithMissingGSI
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "AWS",
                Score = 120,
                ManagerName = "Edward",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithMissingGSI>();
            Assert.AreEqual(0, table.GlobalSecondaryIndexes.Count);

            // Save and Load
            context.Save(employee);
            var emp = context.Load(employee);
            Assert.AreEqual("David", emp.Name);
            Assert.AreEqual("AWS", emp.CompanyName);

            // Query global index for items with GSI Hash-Key = "AWS"
            // Exception message - Unable to locate index [GlobalIndex] on table [DotNetTests-HashRangeTable]
            Assert.ThrowsException<InvalidOperationException>(() => context.Query<EmployeeWithMissingGSI>("AWS", new DynamoDBOperationConfig { IndexName = "GlobalIndex" }).ToList());

            // Scan global index for items with CompanyName = "AWS"
            var emps = context.Scan<EmployeeWithMissingGSI>(
                new List<ScanCondition> { new ScanCondition("CompanyName", ScanOperator.Equal, "AWS") },
                new DynamoDBOperationConfig { IndexName = "GlobalIndex" }).ToList();
            Assert.AreEqual(1, emps.Count);
            Assert.AreEqual("David", emps[0].Name);
            Assert.AreEqual("AWS", emps[0].CompanyName);

            // Scan all rows
            emps = context.Scan<EmployeeWithMissingGSI>().ToList();
            Assert.AreEqual(3, emps.Count);

            // Delete
            context.Delete(employee);
        }

        [TestMethod]
        [TestCategory("DynamoDBv2")]
        public void DisableFetchingTableMetadata_InvalidGSI()
        {
            SetupHashRangeTable();
            var employee = new EmployeeWithInvalidGSI
            {
                Name = "David",
                MiddleName = "Jacob",
                Age = 31,
                CompanyName = "AWS",
                Score = 120,
                ManagerName = "Edward",
            };

            var config = new DynamoDBContextConfig
            {
                IsEmptyStringValueEnabled = true,
                Conversion = DynamoDBEntryConversion.V2,
                DisableFetchingTableMetadata = true
            };
            var context = new DynamoDBContext(Client, config);

            var table = context.GetTargetTable<EmployeeWithInvalidGSI>();
            Assert.AreEqual(1, table.GlobalSecondaryIndexes.Count);
            var gsi = table.GlobalSecondaryIndexes["GlobalIndex"];
            Assert.AreEqual("GlobalIndex", gsi.IndexName);
            Assert.AreEqual(2, gsi.KeySchema.Count);
            Assert.AreEqual("MiddleName", gsi.KeySchema[0].AttributeName);
            Assert.AreEqual(KeyType.HASH, gsi.KeySchema[0].KeyType);
            Assert.AreEqual("Score", gsi.KeySchema[1].AttributeName);
            Assert.AreEqual(KeyType.RANGE, gsi.KeySchema[1].KeyType);

            // Save and Load
            context.Save(employee);
            var emp = context.Load(employee);
            Assert.AreEqual("David", emp.Name);
            Assert.AreEqual("AWS", emp.CompanyName);

            // Query global index for items with the GSI Hash-Key  = "AWS"
            // Exception Message - Query condition missed key schema element: Company
            Assert.ThrowsException<AmazonDynamoDBException>(() => context.Query<EmployeeWithInvalidGSI>("AWS", new DynamoDBOperationConfig { IndexName = "GlobalIndex" }).ToList());

            // Scan global index for items with CompanyName = "AWS"
            var emps = Context.Scan<EmployeeWithInvalidGSI>(
                new List<ScanCondition> { new ScanCondition("CompanyName", ScanOperator.Equal, "AWS") },
                new DynamoDBOperationConfig { IndexName = "GlobalIndex" }).ToList();
            Assert.AreEqual(1, emps.Count);
            Assert.AreEqual("David", emps[0].Name);
            Assert.AreEqual("AWS", emps[0].CompanyName);

            // Scan all rows
            emps = context.Scan<EmployeeWithInvalidGSI>().ToList();
            Assert.AreEqual(3, emps.Count);

            // Delete
            context.Delete(employee);
        }

        private void SetupHashRangeTable()
        {
            TableCache.Clear();
            CleanupTables();
            TableCache.Clear();

            CreateContext(DynamoDBEntryConversion.V2, true, true);

            var employee1 = new AnnotatedEmployee
            {
                Name = "Alan",
                MiddleName = "John",
                Age = 31,
                CompanyName = "Big River",
                Score = 120,
                ManagerName = "Barbara",
            };

            var employee2 = new AnnotatedEmployee
            {
                Name = "Michael",
                MiddleName = "Harry",
                Age = 31,
                CompanyName = "Amazon",
                Score = 150,
                ManagerName = "Edward",
            };

            Context.Save(employee1);
            Context.Save(employee2);
        }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithMissingHashKey
    {
        // Actual Hash Key
        public string Name { get; set; }

        public string MiddleName { get; set;}

        [DynamoDBRangeKey]
        public int Age { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("GlobalIndex", AttributeName = "Company")]
        public string CompanyName { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("GlobalIndex")]
        public int Score { get; set; }

        public string ManagerName { get; set; }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithInvalidHashKey
    {
        // Actual Hash Key
        public string Name { get; set; }

        // Invalid Hash Key
        [DynamoDBHashKey]
        public string MiddleName { get; set; }

        [DynamoDBRangeKey]
        public int Age { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("GlobalIndex", AttributeName = "Company")]
        public string CompanyName { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("GlobalIndex")]
        public int Score { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey("LocalIndex", AttributeName = "Manager")]
        public string ManagerName { get; set; }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithMissingRangeKey
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        public string MiddleName { get; set; }

        // Actual Range key
        public int Age { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("GlobalIndex", AttributeName = "Company")]
        public string CompanyName { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("GlobalIndex")]
        public int Score { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey("LocalIndex", AttributeName = "Manager")]
        public string ManagerName { get; set; }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithInvalidRangeKey
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        // Invalid Range key
        [DynamoDBRangeKey]
        public string MiddleName { get; set; }

        // Actual Range key
        public int Age { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("GlobalIndex", AttributeName = "Company")]
        public string CompanyName { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("GlobalIndex")]
        public int Score { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey("LocalIndex", AttributeName = "Manager")]
        public string ManagerName { get; set; }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithMissingLSI
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        public string MiddleName { get; set; }

        [DynamoDBRangeKey]
        public int Age { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("GlobalIndex", AttributeName = "Company")]
        public string CompanyName { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("GlobalIndex")]
        public int Score { get; set; }

        // Actual LSI
        [DynamoDBProperty(AttributeName = "Manager")]
        public string ManagerName { get; set; }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithInvalidLSI
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        // Invalid LSI
        [DynamoDBLocalSecondaryIndexRangeKey("LocalIndex")]
        public string MiddleName { get; set; }

        [DynamoDBRangeKey]
        public int Age { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("GlobalIndex", AttributeName = "Company")]
        public string CompanyName { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("GlobalIndex")]
        public int Score { get; set; }

        // Actual LSI
        [DynamoDBProperty(AttributeName = "Manager")]
        public string ManagerName { get; set; }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithMissingGSI
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        public string MiddleName { get; set; }

        [DynamoDBRangeKey]
        public int Age { get; set; }

        // Actual GSI
        [DynamoDBProperty(AttributeName = "Company")]
        public string CompanyName { get; set; }

        public int Score { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey("LocalIndex", AttributeName = "Manager")]
        public string ManagerName { get; set; }
    }

    [DynamoDBTable("HashRangeTable")]
    public class EmployeeWithInvalidGSI
    {
        [DynamoDBHashKey]
        public string Name { get; set; }

        // Invalid GSI
        [DynamoDBGlobalSecondaryIndexHashKey("GlobalIndex")]
        public string MiddleName { get; set; }

        [DynamoDBRangeKey]
        public int Age { get; set; }

        // Actual GSI
        [DynamoDBProperty(AttributeName = "Company")]
        public string CompanyName { get; set; }

        [DynamoDBGlobalSecondaryIndexRangeKey("GlobalIndex")]
        public int Score { get; set; }

        [DynamoDBLocalSecondaryIndexRangeKey("LocalIndex", AttributeName = "Manager")]
        public string ManagerName { get; set; }
    }
}
