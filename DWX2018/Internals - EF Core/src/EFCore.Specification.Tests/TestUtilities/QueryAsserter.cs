﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class QueryAsserter<TContext> : QueryAsserterBase
        where TContext : DbContext
    {
        private readonly Func<TContext> _contextCreator;
        private readonly Dictionary<Type, Func<dynamic, object>> _entitySorters;
        private readonly Dictionary<Type, Action<dynamic, dynamic>> _entityAsserters;
        private readonly IncludeQueryResultAsserter _includeResultAsserter;

        public QueryAsserter(
            Func<TContext> contextCreator,
            IExpectedData expectedData,
            Dictionary<Type, Func<dynamic, object>> entitySorters,
            Dictionary<Type, Action<dynamic, dynamic>> entityAsserters)
        {
            _contextCreator = contextCreator;
            ExpectedData = expectedData;

            _entitySorters = entitySorters ?? new Dictionary<Type, Func<dynamic, object>>();
            _entityAsserters = entityAsserters ?? new Dictionary<Type, Action<dynamic, dynamic>>();

            SetExtractor = new DefaultSetExtractor();
            _includeResultAsserter = new IncludeQueryResultAsserter(_entitySorters, _entityAsserters);
        }

        #region AssertSingleResult

        // one argument

        public virtual void AssertSingleResult<TItem1>(
            Func<IQueryable<TItem1>, object> query,
            Action<object, object> asserter = null,
            int entryCount = 0)
            where TItem1 : class
            => AssertSingleResult(query, query, asserter, entryCount);

        public override void AssertSingleResult<TItem1>(
            Func<IQueryable<TItem1>, object> actualQuery,
            Func<IQueryable<TItem1>, object> expectedQuery,
            Action<object, object> asserter = null,
            int entryCount = 0)
        {
            using (var context = _contextCreator())
            {
                var actual = actualQuery(SetExtractor.Set<TItem1>(context));
                var expected = expectedQuery(ExpectedData.Set<TItem1>());

                if (asserter == null
                    && expected != null)
                {
                    _entityAsserters.TryGetValue(expected.GetType(), out asserter);
                }

                if (asserter != null)
                {
                    asserter(expected, actual);
                }
                else
                {
                    Assert.Equal(expected, actual);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        // two arguments

        public virtual void AssertSingleResult<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, object> query,
            Action<object, object> asserter = null,
            int entryCount = 0)
            where TItem1 : class
            where TItem2 : class
            => AssertSingleResult(query, query, asserter, entryCount);

        public override void AssertSingleResult<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, object> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, object> expectedQuery,
            Action<object, object> asserter = null,
            int entryCount = 0)
        {
            using (var context = _contextCreator())
            {
                var actual = actualQuery(
                    SetExtractor.Set<TItem1>(context),
                    SetExtractor.Set<TItem2>(context));

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>());

                if (asserter == null
                    && expected != null)
                {
                    _entityAsserters.TryGetValue(expected.GetType(), out asserter);
                }

                if (asserter != null)
                {
                    asserter(expected, actual);
                }
                else
                {
                    Assert.Equal(expected, actual);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        // three arguments

        public virtual void AssertSingleResult<TItem1, TItem2, TItem3>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, object> query,
            Action<object, object> asserter = null,
            int entryCount = 0)
            where TItem1 : class
            where TItem2 : class
            where TItem3 : class
            => AssertSingleResult(query, query, asserter, entryCount);

        public override void AssertSingleResult<TItem1, TItem2, TItem3>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, object> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, object> expectedQuery,
            Action<object, object> asserter = null,
            int entryCount = 0)
        {
            using (var context = _contextCreator())
            {
                var actual = actualQuery(
                    SetExtractor.Set<TItem1>(context),
                    SetExtractor.Set<TItem2>(context),
                    SetExtractor.Set<TItem3>(context));

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>(),
                    ExpectedData.Set<TItem3>());

                if (asserter == null
                    && expected != null)
                {
                    _entityAsserters.TryGetValue(expected.GetType(), out asserter);
                }

                if (asserter != null)
                {
                    asserter(expected, actual);
                }
                else
                {
                    Assert.Equal(expected, actual);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        #endregion

        #region AssertSingleResult - async

        // one argument

        public virtual Task AssertSingleResultAsync<TItem1, TResult>(
            Func<IQueryable<TItem1>, Task<TResult>> query,
            Action<object, object> asserter = null,
            int entryCount = 0)
            where TItem1 : class
            => AssertSingleResultAsync(query, query, asserter, entryCount);

        public override async Task AssertSingleResultAsync<TItem1, TResult>(
            Func<IQueryable<TItem1>, Task<TResult>> actualQuery,
            Func<IQueryable<TItem1>, Task<TResult>> expectedQuery,
            Action<object, object> asserter = null,
            int entryCount = 0)
        {
            using (var context = _contextCreator())
            {
                var actual = await actualQuery(SetExtractor.Set<TItem1>(context));
                var expected = await expectedQuery(ExpectedData.Set<TItem1>());

                if (asserter == null
                    && expected != null)
                {
                    _entityAsserters.TryGetValue(expected.GetType(), out asserter);
                }

                if (asserter != null)
                {
                    asserter(expected, actual);
                }
                else
                {
                    Assert.Equal(expected, actual);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        // two arguments

        public virtual Task AssertSingleResultAsync<TItem1, TItem2, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, Task<TResult>> query,
            Action<object, object> asserter = null,
            int entryCount = 0)
            where TItem1 : class
            where TItem2 : class
            => AssertSingleResultAsync(query, query, asserter, entryCount);

        public override async Task AssertSingleResultAsync<TItem1, TItem2, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, Task<TResult>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, Task<TResult>> expectedQuery,
            Action<object, object> asserter = null,
            int entryCount = 0)
        {
            using (var context = _contextCreator())
            {
                var actual = await actualQuery(
                    SetExtractor.Set<TItem1>(context),
                    SetExtractor.Set<TItem2>(context));

                var expected = await expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>());

                if (asserter == null
                    && expected != null)
                {
                    _entityAsserters.TryGetValue(expected.GetType(), out asserter);
                }

                if (asserter != null)
                {
                    asserter(expected, actual);
                }
                else
                {
                    Assert.Equal(expected, actual);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        // three arguments

        public virtual Task AssertSingleResultAsync<TItem1, TItem2, TItem3, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, Task<TResult>> query,
            Action<object, object> asserter = null,
            int entryCount = 0)
            where TItem1 : class
            where TItem2 : class
            where TItem3 : class
            => AssertSingleResultAsync(query, query, asserter, entryCount);

        public override async Task AssertSingleResultAsync<TItem1, TItem2, TItem3, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, Task<TResult>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, Task<TResult>> expectedQuery,
            Action<object, object> asserter = null,
            int entryCount = 0)
        {
            using (var context = _contextCreator())
            {
                var actual = await actualQuery(
                    SetExtractor.Set<TItem1>(context),
                    SetExtractor.Set<TItem2>(context),
                    SetExtractor.Set<TItem3>(context));

                var expected = await expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>(),
                    ExpectedData.Set<TItem3>());

                if (asserter == null
                    && expected != null)
                {
                    _entityAsserters.TryGetValue(expected.GetType(), out asserter);
                }

                if (asserter != null)
                {
                    asserter(expected, actual);
                }
                else
                {
                    Assert.Equal(expected, actual);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        #endregion

        #region AssertQuery

        // one argument

        public virtual Task AssertQuery<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<object>> query,
            Func<dynamic, object> elementSorter = null,
            Action<dynamic, dynamic> elementAsserter = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryAsync(query, query, elementSorter, elementAsserter, assertOrder, entryCount, isAsync);

        public override async Task AssertQueryAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<object>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<object>> expectedQuery,
            Func<dynamic, object> elementSorter = null,
            Action<dynamic, dynamic> elementAsserter = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(SetExtractor.Set<TItem1>(context)).ToArrayAsync()
                    : actualQuery(SetExtractor.Set<TItem1>(context)).ToArray();

                var expected = expectedQuery(ExpectedData.Set<TItem1>()).ToArray();

                var firstNonNullableElement = expected.FirstOrDefault(e => e != null);
                if (firstNonNullableElement != null)
                {
                    if (!assertOrder
                        && elementSorter == null)
                    {
                        _entitySorters.TryGetValue(firstNonNullableElement.GetType(), out elementSorter);
                    }

                    if (elementAsserter == null)
                    {
                        _entityAsserters.TryGetValue(firstNonNullableElement.GetType(), out elementAsserter);
                    }
                }

                TestHelpers.AssertResults(
                    expected,
                    actual,
                    elementSorter,
                    elementAsserter,
                    assertOrder);

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        // two arguments

        public virtual Task AssertQuery<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<object>> query,
            Func<dynamic, object> elementSorter = null,
            Action<dynamic, dynamic> elementAsserter = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            => AssertQueryAsync(query, query, elementSorter, elementAsserter, assertOrder, entryCount, isAsync);

        public override async Task AssertQueryAsync<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<object>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<object>> expectedQuery,
            Func<dynamic, object> elementSorter = null,
            Action<dynamic, dynamic> elementAsserter = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToArrayAsync()
                    : actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToArray();

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>()).ToArray();

                var firstNonNullableElement = expected.FirstOrDefault(e => e != null);
                if (firstNonNullableElement != null)
                {
                    if (!assertOrder
                        && elementSorter == null)
                    {
                        _entitySorters.TryGetValue(firstNonNullableElement.GetType(), out elementSorter);
                    }

                    if (elementAsserter == null)
                    {
                        _entityAsserters.TryGetValue(firstNonNullableElement.GetType(), out elementAsserter);
                    }
                }

                TestHelpers.AssertResults(
                    expected,
                    actual,
                    elementSorter,
                    elementAsserter,
                    assertOrder);

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        // three arguments
        public virtual Task AssertQuery<TItem1, TItem2, TItem3>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, IQueryable<object>> query,
            Func<dynamic, object> elementSorter = null,
            Action<dynamic, dynamic> elementAsserter = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            where TItem3 : class
            => AssertQueryAsync(query, query, elementSorter, elementAsserter, assertOrder, entryCount, isAsync);

        public override async Task AssertQueryAsync<TItem1, TItem2, TItem3>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, IQueryable<object>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, IQueryable<object>> expectedQuery,
            Func<dynamic, object> elementSorter = null,
            Action<dynamic, dynamic> elementAsserter = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context),
                        SetExtractor.Set<TItem3>(context)).ToArrayAsync()
                    : actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context),
                        SetExtractor.Set<TItem3>(context)).ToArray();

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>(),
                    ExpectedData.Set<TItem3>()).ToArray();

                var firstNonNullableElement = expected.FirstOrDefault(e => e != null);
                if (firstNonNullableElement != null)
                {
                    if (!assertOrder
                        && elementSorter == null)
                    {
                        _entitySorters.TryGetValue(firstNonNullableElement.GetType(), out elementSorter);
                    }

                    if (elementAsserter == null)
                    {
                        _entityAsserters.TryGetValue(firstNonNullableElement.GetType(), out elementAsserter);
                    }
                }

                TestHelpers.AssertResults(
                    expected,
                    actual,
                    elementSorter,
                    elementAsserter,
                    assertOrder);

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());
            }
        }

        #endregion

        #region AssertQueryScalar

        // one argument

        public virtual Task AssertQueryScalarAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<int>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<int>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<int>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryScalarAsync<TItem1, int>(actualQuery, expectedQuery, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<long>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<short>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<bool>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TResult>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            where TResult : struct
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public override async Task AssertQueryScalarAsync<TItem1, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TResult>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TResult>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(SetExtractor.Set<TItem1>(context)).ToArrayAsync()
                    : actualQuery(SetExtractor.Set<TItem1>(context)).ToArray();

                var expected = expectedQuery(ExpectedData.Set<TItem1>()).ToArray();
                TestHelpers.AssertResults(
                    expected,
                    actual,
                    e => e,
                    Assert.Equal,
                    assertOrder);
            }
        }

        // two arguments

        public virtual Task AssertQueryScalarAsync<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<int>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<int>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<int>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            => AssertQueryScalarAsync<TItem1, TItem2, int>(actualQuery, expectedQuery, assertOrder, isAsync);

        public override async Task AssertQueryScalarAsync<TItem1, TItem2, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TResult>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TResult>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToArrayAsync()
                    : actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToArray();

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>()).ToArray();

                TestHelpers.AssertResults(
                    expected,
                    actual,
                    e => e,
                    Assert.Equal,
                    assertOrder);
            }
        }

        // three arguments

        public virtual Task AssertQueryScalarAsync<TItem1, TItem2, TItem3>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, IQueryable<int>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            where TItem3 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public override async Task AssertQueryScalarAsync<TItem1, TItem2, TItem3, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, IQueryable<TResult>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TItem3>, IQueryable<TResult>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context),
                        SetExtractor.Set<TItem3>(context)).ToArrayAsync()
                    : actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context),
                        SetExtractor.Set<TItem3>(context)).ToArray();

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>(),
                    ExpectedData.Set<TItem3>()).ToArray();

                TestHelpers.AssertResults(
                    expected,
                    actual,
                    e => e,
                    Assert.Equal,
                    assertOrder);
            }
        }

        #endregion

        #region AssertQueryNullableScalar

        // one argument

        public virtual Task AssertQueryScalarAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<int?>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<int?>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<int?>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            => AssertQueryScalarAsync<TItem1, int>(actualQuery, expectedQuery, assertOrder, isAsync);

        public override async Task AssertQueryScalarAsync<TItem1, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TResult?>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TResult?>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(SetExtractor.Set<TItem1>(context)).ToArrayAsync()
                    : actualQuery(SetExtractor.Set<TItem1>(context)).ToArray();
                var expected = expectedQuery(ExpectedData.Set<TItem1>()).ToArray();
                TestHelpers.AssertResultsNullable(
                    expected,
                    actual,
                    e => e,
                    Assert.Equal,
                    assertOrder);
            }
        }

        // two arguments

        public virtual Task AssertQueryScalarAsync<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<int?>> query,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            => AssertQueryScalarAsync(query, query, assertOrder, isAsync);

        public virtual Task AssertQueryScalarAsync<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<int?>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<int?>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            => AssertQueryScalarAsync<TItem1, TItem2, int>(actualQuery, expectedQuery, assertOrder, isAsync);

        public override async Task AssertQueryScalarAsync<TItem1, TItem2, TResult>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TResult?>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<TResult?>> expectedQuery,
            bool assertOrder = false,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToArrayAsync()
                    : actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToArray();

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>()).ToArray();
                TestHelpers.AssertResultsNullable(
                    expected,
                    actual,
                    e => e,
                    Assert.Equal,
                    assertOrder);
            }
        }

        #endregion

        #region AssertIncludeQuery

        public Task<List<object>> AssertIncludeQueryAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<object>> query,
            List<IExpectedInclude> expectedIncludes,
            Func<dynamic, object> elementSorter = null,
            List<Func<dynamic, object>> clientProjections = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
            where TItem1 : class
            => AssertIncludeQueryAsync(query, query, expectedIncludes, elementSorter, clientProjections, assertOrder, entryCount, isAsync);

        public override async Task<List<object>> AssertIncludeQueryAsync<TItem1>(
            Func<IQueryable<TItem1>, IQueryable<object>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<object>> expectedQuery,
            List<IExpectedInclude> expectedIncludes,
            Func<dynamic, object> elementSorter = null,
            List<Func<dynamic, object>> clientProjections = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(
                        SetExtractor.Set<TItem1>(context)).ToListAsync()
                    : actualQuery(
                        SetExtractor.Set<TItem1>(context)).ToList();

                var expected = expectedQuery(ExpectedData.Set<TItem1>()).ToList();

                if (!assertOrder)
                {
                    if (elementSorter == null)
                    {
                        var firstNonNullableElement = expected.FirstOrDefault(e => e != null);
                        if (firstNonNullableElement != null)
                        {
                            _entitySorters.TryGetValue(firstNonNullableElement.GetType(), out elementSorter);
                        }
                    }

                    if (elementSorter != null)
                    {
                        actual = actual.OrderBy(elementSorter).ToList();
                        expected = expected.OrderBy(elementSorter).ToList();
                    }
                }

                if (clientProjections != null)
                {
                    foreach (var clientProjection in clientProjections)
                    {
                        var projectedActual = actual.Select(clientProjection).ToList();
                        var projectedExpected = expected.Select(clientProjection).ToList();

                        _includeResultAsserter.AssertResult(projectedExpected, projectedActual, expectedIncludes);
                    }
                }
                else
                {
                    _includeResultAsserter.AssertResult(expected, actual, expectedIncludes);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());

                return actual;
            }
        }

        public Task<List<object>> AssertIncludeQueryAsync<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<object>> query,
            List<IExpectedInclude> expectedIncludes,
            Func<dynamic, object> elementSorter = null,
            List<Func<dynamic, object>> clientProjections = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
            where TItem1 : class
            where TItem2 : class
            => AssertIncludeQueryAsync(query, query, expectedIncludes, elementSorter, clientProjections, assertOrder, entryCount, isAsync);

        public override async Task<List<object>> AssertIncludeQueryAsync<TItem1, TItem2>(
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<object>> actualQuery,
            Func<IQueryable<TItem1>, IQueryable<TItem2>, IQueryable<object>> expectedQuery,
            List<IExpectedInclude> expectedIncludes,
            Func<dynamic, object> elementSorter = null,
            List<Func<dynamic, object>> clientProjections = null,
            bool assertOrder = false,
            int entryCount = 0,
            bool isAsync = false)
        {
            using (var context = _contextCreator())
            {
                var actual = isAsync
                    ? await actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToListAsync()
                    : actualQuery(
                        SetExtractor.Set<TItem1>(context),
                        SetExtractor.Set<TItem2>(context)).ToList();

                var expected = expectedQuery(
                    ExpectedData.Set<TItem1>(),
                    ExpectedData.Set<TItem2>()).ToList();

                if (!assertOrder)
                {
                    if (elementSorter == null)
                    {
                        var firstNonNullableElement = expected.FirstOrDefault(e => e != null);
                        if (firstNonNullableElement != null)
                        {
                            _entitySorters.TryGetValue(firstNonNullableElement.GetType(), out elementSorter);
                        }
                    }

                    if (elementSorter != null)
                    {
                        actual = actual.OrderBy(elementSorter).ToList();
                        expected = expected.OrderBy(elementSorter).ToList();
                    }
                }

                if (clientProjections != null)
                {
                    foreach (var clientProjection in clientProjections)
                    {
                        var projectedActual = actual.Select(clientProjection).ToList();
                        var projectedExpected = expected.Select(clientProjection).ToList();

                        _includeResultAsserter.AssertResult(projectedExpected, projectedActual, expectedIncludes);
                    }
                }
                else
                {
                    _includeResultAsserter.AssertResult(expected, actual, expectedIncludes);
                }

                Assert.Equal(entryCount, context.ChangeTracker.Entries().Count());

                return actual;
            }
        }

        #endregion

        private class DefaultSetExtractor : ISetExtractor
        {
            public override IQueryable<TEntity> Set<TEntity>(DbContext context)
            {
                var entityOrQueryType = context.Model.FindEntityType(typeof(TEntity));

                return entityOrQueryType.IsQueryType
                    ? (IQueryable<TEntity>)context.Query<TEntity>()
                    : context.Set<TEntity>();
            }
        }
    }
}
