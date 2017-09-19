﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System.Collections;
using NHibernate.Criterion;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH1178
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : BugTestCase
	{
		public override string BugNumber
		{
			get { return "NH1178"; }
		}

		protected override void OnSetUp()
		{
			using (ISession s = OpenSession())
			using (ITransaction tx = s.BeginTransaction())
			{
				Foo f1 = new Foo();
				f1.Word = "mono";
				f1.Number = 0;
				s.Save(f1);

				Foo f2 = new Foo();
				f2.Word = "mono";
				f2.Number = 1000;
				s.Save(f2);

				Foo f3 = new Foo();
				f3.Word = "mono";
				f3.Number = 0;
				s.Save(f3);

				Foo f4 = new Foo();
				f4.Word = null;
				f4.Number = 1000;
				s.Save(f4);

				tx.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (ISession s = OpenSession())

			using (ITransaction tx = s.BeginTransaction())
			{
				s.Delete("from Foo");
				tx.Commit();
			}
		}

		[Test]
		public async Task ExcludeNullsAndZeroesAsync()
		{
			using (ISession s = OpenSession())
			{
				Example example = Example.Create(new Foo(1000, "mono")).ExcludeZeroes().ExcludeNulls();
				IList results = await (s.CreateCriteria(typeof (Foo)).Add(example).ListAsync());
				Assert.AreEqual(1, results.Count);
			}

			using (ISession s = OpenSession())
			{
				Example example = Example.Create(new Foo(1000, "mono")).ExcludeNulls().ExcludeZeroes();
				IList results = await (s.CreateCriteria(typeof (Foo)).Add(example).ListAsync());
				Assert.AreEqual(1, results.Count);
			}
		}
	}
}