using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdemolaTyperTest.BDD
    {
        public delegate void MethodThatThrows();

        public static class BDDSpecExtensions
        {
            public static void ShouldBeFalse(this bool condition)
            {
                Assert.IsFalse(condition);
            }

            public static void ShouldBeTrue(this bool condition)
            {
                Assert.IsTrue(condition);
            }

            public static object ShouldEqual(this object actual, object expected)
            {
                Assert.AreEqual(expected, actual);
                return expected;
            }

            public static object ShouldNotEqual(this object actual, object expected)
            {
                Assert.AreNotEqual(expected, actual);
                return expected;
            }

            public static void ShouldBeNull(this object anObject)
            {
                Assert.IsNull(anObject);
            }

            public static void ShouldNotBeNull(this object anObject)
            {
                Assert.IsNotNull(anObject);
            }

            public static object ShouldBeTheSameAs(this object actual, object expected)
            {
                Assert.AreSame(expected, actual);
                return expected;
            }

            public static object ShouldNotBeTheSameAs(this object actual, object expected)
            {
                Assert.AreNotSame(expected, actual);
                return expected;
            }

            public static void ShouldBeOfType(this object actual, Type expected)
            {
                Assert.IsInstanceOfType(actual, expected);
            }

            public static void ShouldBe(this object actual, Type expected)
            {
                Assert.IsInstanceOfType(actual, expected);
            }

            public static void ShouldNotBeOfType(this object actual, Type expected)
            {
                Assert.IsNotInstanceOfType(actual, expected);
            }

            public static void ShouldNotContain(this IList collection, object expected)
            {
                CollectionAssert.DoesNotContain(collection, expected);
            }

            public static void ShouldStartWith(this string actual, string expected)
            {
                StringAssert.StartsWith(expected, actual);
            }

            public static void ShouldEndWith(this string actual, string expected)
            {
                StringAssert.EndsWith(expected, actual);
            }

            public static void ShouldBeSurroundedWith(this string actual, string expectedStartDelimiter, string expectedEndDelimiter)
            {
                StringAssert.StartsWith(expectedStartDelimiter, actual);
                StringAssert.EndsWith(expectedEndDelimiter, actual);
            }

            public static void ShouldBeSurroundedWith(this string actual, string expectedDelimiter)
            {
                StringAssert.StartsWith(expectedDelimiter, actual);
                StringAssert.EndsWith(expectedDelimiter, actual);
            }

            public static void ShouldContainErrorMessage(this Exception exception, string expected)
            {
                StringAssert.Contains(expected, exception.Message);
            }

            public static Exception ShouldBeThrownBy(this Type exceptionType, MethodThatThrows method)
            {
                Exception exception = method.GetException();

                Assert.IsNotNull(exception);
                Assert.AreEqual(exceptionType, exception.GetType());

                return exception;
            }

            public static Exception GetException(this MethodThatThrows method)
            {
                Exception exception = null;

                try
                {
                    method();
                }
                catch (Exception e)
                {
                    exception = e;
                }

                return exception;
            }
        }
    }