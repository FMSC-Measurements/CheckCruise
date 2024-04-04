using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using FluentAssertions;
using Xunit;
using CruiseDAL;
using CruiseDAL.V2.Models;
using AutoBogus;

namespace CheckCruise.Test
{
    public class DataBaseCommands_Test : TestBase
    {
        public DataBaseCommands_Test(ITestOutputHelper output) : base(output)
        {

        }


        [Fact]
        public void GetUnitStrata()
        {
            var init = new DatabaseInitializer_V2();

            using var db = init.CreateDatabase();
            var dbCmds = new dataBaseCommands(db);

            var unitStrata = dbCmds.GetUnitStrata();
            unitStrata.Should().NotBeEmpty();

            unitStrata.Should().HaveCount(init.UnitStrata.Count());

            unitStrata.All(x => !String.IsNullOrEmpty(x.CuttingUnitCode)).Should().BeTrue();
            unitStrata.All(x => !String.IsNullOrEmpty(x.StratumCode)).Should().BeTrue();
            unitStrata.All(x => !String.IsNullOrEmpty(x.Method)).Should().BeTrue();
            unitStrata.All(x => x.CuttingUnit_CN > 0).Should().BeTrue();
            unitStrata.All(x => x.Stratum_CN > 0).Should().BeTrue();

        }

        [Theory]
        [InlineData("Results")]
        [InlineData("Tolerances")]
        public void doesTableExist(string tableName)
        {
            using var db = new DAL();

            var dbCmds = new dataBaseCommands(db);

            dbCmds.doesTableExist(tableName).Should().BeFalse();

            dbCmds.createNewTable(tableName);

            dbCmds.doesTableExist(tableName).Should().BeTrue();
        }

        [Fact]
        public void getCruiserInitials()
        {
            var db = new DAL();

            var dbCmds = new dataBaseCommands(db);
            dbCmds.createNewTable("Results");

            var initials = dbCmds.getCruiserInitials();

        }

        [Fact]
        public void deleteUnit()
        {
            var init = new DatabaseInitializer_V2();

            using var db = init.CreateDatabase();
            var dbCmds = new dataBaseCommands(db);

            var tree = new Tree
            {
                CuttingUnit_CN = 1,
                Stratum_CN = 1,
                TreeNumber = 1,
            };
            db.Insert(tree);

            var log = new Log
            {
                Tree_CN = tree.Tree_CN.Value,
                LogNumber = "1",
            };
            db.Insert(log);

            db.From<Tree>().Count().Should().Be(1);
            db.From<Log>().Count().Should().Be(1);

            dbCmds.deleteUnit("u1", "st1");

            db.From<Tree>().Count().Should().Be(0);
            db.From<Log>().Count().Should().Be(0);

        }

        [Fact]
        public void deleteTreeCalculatedValues()
        {
            var init = new DatabaseInitializer_V2();

            using var db = init.CreateDatabase();
            var dbCmds = new dataBaseCommands(db);

            var tcvFaker = new AutoFaker<TreeCalculatedValues>();

            var tcv = tcvFaker.Generate();
            db.Insert(tcv);

            db.From<TreeCalculatedValues>().Count().Should().Be(1);

            dbCmds.deleteTreeCalculatedValues();

            db.From<TreeCalculatedValues>().Count().Should().Be(0);
        }

        [Fact]
        public void getTolerances()
        {
            using var db = new DAL();

            var dbCmds = new dataBaseCommands(db);
            dbCmds.createNewTable("Tolerances");

            var tolFaker = new AutoFaker<TolerancesList>();
            var tol = tolFaker.Generate();

            db.Insert(tol);

            var tols = dbCmds.getTolerances();
            tols.Single().Should().BeEquivalentTo(tol);
        }

        [Fact]
        public void saveTolerances()
        {
            using var db = new DAL();

            var dbCmds = new dataBaseCommands(db);
            dbCmds.createNewTable("Tolerances");

            var tolFaker = new AutoFaker<TolerancesList>();
            var tol = tolFaker.Generate();

            dbCmds.saveTolerances(new[] { tol });

            var tolAgain = dbCmds.getTolerances().Single();
            tolAgain.Should().BeEquivalentTo(tol);
        }

        [Fact]
        public void getResultsTable()
        {
            using var db = new DAL();

            var dbCmds = new dataBaseCommands(db);
            dbCmds.createNewTable("Results");

            var resultFaker = new AutoFaker<ResultsList>();
            var r = resultFaker.Generate();

            db.Insert(r);

            var results = dbCmds.getResultsTable("", "");

            var rAgain = results.Single();
            rAgain.Should().BeEquivalentTo(r);
        }

        [Fact]
        public void getResultsTable_WithGroupBy()
        {
            using var db = new DAL();

            var dbCmds = new dataBaseCommands(db);
            dbCmds.createNewTable("Results");

            var resultFaker = new AutoFaker<ResultsList>();
            var r = resultFaker.Generate();

            db.Insert(r);

            var results = dbCmds.getResultsTable("R_TreeSpecies", "");

            var rAgain = results.Single();
            rAgain.Should().BeEquivalentTo(r);
        }

        [Fact]
        public void getResultsTable_WithInitialsParam()
        {
            using var db = new DAL();

            var dbCmds = new dataBaseCommands(db);
            dbCmds.createNewTable("Results");

            var resultFaker = new AutoFaker<ResultsList>();
            var r = resultFaker.Generate();

            db.Insert(r);

            var results = dbCmds.getResultsTable("", r.R_MarkerInitials);

            var rAgain = results.Single();
            rAgain.Should().BeEquivalentTo(r);
        }

        [Fact]
        public void saveResults()
        {
            using var db = new DAL();

            var dbCmds = new dataBaseCommands(db);
            dbCmds.createNewTable("Results");

            var resultFaker = new AutoFaker<ResultsList>();
            var r = resultFaker.Generate();

            dbCmds.saveResults(new[] { r });

            var rAgain = db.From<ResultsList>().Query().Single();
            rAgain.Should().BeEquivalentTo(r);
        }
    }
}
