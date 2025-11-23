using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Couriers.Common.Xml.Tests
{
    /// <summary>
    /// Contains the tests regarding the <see cref="XmlHelpers"/>
    /// </summary>
    public class XmlHelpersUnitTests
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of <see cref="XmlHelpersUnitTests"/>
        /// </summary>
        public XmlHelpersUnitTests() : base()
        {

        }

        #endregion

        #region Public Methods

        #region Test Methods

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.Deserialize{T}(XContainer)"/> method is called, 
        /// with <see langword="null"/> argument, an <see cref="Exception"/> is thrown
        /// </summary>
        [Fact]
        public void Deserialize_WithNullArgument_ThrowsException()
        {
            var xmlContainer = default(XContainer?);

            Assert.ThrowsAny<Exception>(() => XmlHelpers.Deserialize<object>(xmlContainer!));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.Deserialize{T}(XContainer)"/> method is called, 
        /// with a XML text, that matches the specified type, the expected result is returned
        /// </summary>
        [Fact]
        public void Deserialize_WithValidXml_ReturnsExpectedResult()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var value = @$"<TestOrder
                              xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                              xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                              <OrderNumber>{orderNumber}</OrderNumber>
                         </TestOrder>";

            using var textReader = new StringReader(value);

            var xmlContainer = XElement.Load(textReader);

            var order = XmlHelpers.Deserialize<TestOrder>(xmlContainer);

            Assert.NotNull(order);

            Assert.Equal(orderNumber, order.OrderNumber);
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.SerializeToXElement{T}(T, string, string)"/> method is called, 
        /// with <see langword="null"/> argument, an <see cref="Exception"/> is thrown
        /// </summary>
        [Fact]
        public void SerializeToXElement_WithNullArgument_ThrowsException()
        {
            var order = default(TestOrder?);

            var defaultPrefix = TestHelpers.RenerateRandomWord(4);

            var defaultNamespace = TestHelpers.RenerateRandomWord(4);

            Assert.ThrowsAny<Exception>(() => XmlHelpers.SerializeToXElement(order!, defaultPrefix, defaultNamespace));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.SerializeToXElement{T}(T, string, string)"/> method is called, 
        /// with <see langword="null"/> prefix, an <see cref="Exception"/> is thrown
        /// </summary>
        [Fact]
        public void SerializeToXElement_WithNullDefaultPrefix_ThrowsException()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var order = new TestOrder()
            {
                OrderNumber = orderNumber
            };

            var defaultNamespace = TestHelpers.RenerateRandomWord(4);

            Assert.ThrowsAny<Exception>(() => XmlHelpers.SerializeToXElement(order, null!, defaultNamespace));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.SerializeToXElement{T}(T, string, string)"/> method is called, 
        /// with empty namespace, an <see cref="Exception"/> is thrown
        /// </summary>
        /// <param name="value">The empty <see cref="string"/> value</param>
        [Theory]
        [MemberData(nameof(TestConstants.EmptyStringValues), MemberType = typeof(TestConstants))]
        public void SerializeToXElement_WithEmptyDefaultNamespace_ThrowsException(string? value)
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var order = new TestOrder()
            {
                OrderNumber = orderNumber
            };

            var defaultPrefix = TestHelpers.RenerateRandomWord(4);

            Assert.ThrowsAny<Exception>(() => XmlHelpers.SerializeToXElement(order, defaultPrefix, value!));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.SerializeToXElement{T}(T, string, string)"/> method is called, 
        /// with valid arguments, the expected result is returned
        /// </summary>
        [Fact]
        public void SerializeToXElement_WithValidXml_ReturnsExpectedResult()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var order = new TestOrder()
            {
                OrderNumber = orderNumber
            };

            var defaultPrefix = TestHelpers.RenerateRandomWord(4);

            var defaultNamespace = TestHelpers.RenerateRandomWord(4);

            var xmlElement = XmlHelpers.SerializeToXElement(order, defaultPrefix, defaultNamespace);

            Assert.NotNull(xmlElement);

            var namespaceAttribute = xmlElement.FirstAttribute;

            Assert.NotNull(namespaceAttribute);

            Assert.Equal(namespaceAttribute.Value, defaultNamespace);

            Assert.Equal(xmlElement.Value, orderNumber);
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.ToXml(object, XmlSerializerNamespaces, XmlWriterSettings)"/> method is called,
        /// with any <see langword="null"/> argument, an <see cref="Exception"/> is thrown
        /// </summary>
        [Fact]
        public void ToXml_WithNullArguments_ThrowsException()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var order = new TestOrder()
            {
                OrderNumber = orderNumber
            };

            var xmlNamespaces = new XmlSerializerNamespaces();

            var xmlWriterSettings = new XmlWriterSettings();

            Assert.ThrowsAny<Exception>(() => XmlHelpers.ToXml(null!, xmlNamespaces, xmlWriterSettings));

            Assert.ThrowsAny<Exception>(() => XmlHelpers.ToXml(order, null!, xmlWriterSettings));

            Assert.ThrowsAny<Exception>(() => XmlHelpers.ToXml(order, xmlNamespaces, null!));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.ToXml(object, XmlSerializerNamespaces, XmlWriterSettings)"/>
        /// and <see cref="XmlHelpers.ToXml(object, XmlSerializerNamespaces)"/> methods are called, 
        /// with valid arguments, the expected result is returned
        /// </summary>
        [Fact]
        public void ToXml_WithAgruments_ReturnsExpectedResult()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var order = new TestOrder()
            {
                OrderNumber = orderNumber
            };

            var xmlNamespaces = new XmlSerializerNamespaces();

            var xmlWriterSettings = new XmlWriterSettings();

            var xml = XmlHelpers.ToXml(order, xmlNamespaces, xmlWriterSettings);

            Assert.False(string.IsNullOrWhiteSpace(xml));

            xml = XmlHelpers.ToXml(order, xmlNamespaces);

            Assert.False(string.IsNullOrWhiteSpace(xml));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.FromXml{T}(string)"/> and <see cref="XmlHelpers.FromXml(string, Type)"/> methods are called, 
        /// with a XML text, that matches the specified type, the expected result is returned
        /// </summary>
        [Fact]
        public void FromXml_WithValidXml_ReturnsExpectedResult()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var value = @$"<TestOrder
                              xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                              xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                              <OrderNumber>{orderNumber}</OrderNumber>
                         </TestOrder>";


            var order = XmlHelpers.FromXml<TestOrder>(value);

            Assert.NotNull(order);

            Assert.Equal(orderNumber, order.OrderNumber);

            var castedOrder = (TestOrder)XmlHelpers.FromXml(value, typeof(TestOrder));

            Assert.NotNull(castedOrder);

            Assert.Equal(orderNumber, castedOrder.OrderNumber);
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.FromXml(string, Type)"/> method is called, 
        /// with empty XML text, an <see cref="Exception"/> is thrown
        /// </summary>
        /// <param name="value">The empty <see cref="string"/> value</param>
        [Theory]
        [MemberData(nameof(TestConstants.EmptyStringValues), MemberType = typeof(TestConstants))]
        public void FromXml_WithEmptyXml_ThrowsException(string? value)
        {
            Assert.ThrowsAny<Exception>(() => XmlHelpers.FromXml(value!, typeof(TestOrder)));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.FromXml(string, Type)"/> method is called, 
        /// with <see langword="null"/> type, an <see cref="Exception"/> is thrown
        /// </summary>
        [Fact]
        public void FromXml_WithNullType_ThrowsException()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var xml = @$"<TestOrder
                              xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                              xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                              <OrderNumber>{orderNumber}</OrderNumber>
                         </TestOrder>";

            Assert.ThrowsAny<Exception>(() => XmlHelpers.FromXml(xml, null!));
        }

        /// <summary>
        /// Validates that when the <see cref="XmlHelpers.FromXml{T}(string)"/> and <see cref="XmlHelpers.FromXml(string, Type)"/> methods are called, 
        /// with a XML text and a <see cref="string"/> as the type, the expected result is returned
        /// </summary>
        [Fact]
        public void FromXml_WithValidXml_ReturnsTheSameXml()
        {
            var orderNumber = TestHelpers.GenerateRandomString(5);

            var value = @$"<TestOrder
                              xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
                              xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                              <OrderNumber>{orderNumber}</OrderNumber>
                         </TestOrder>";

            var order = XmlHelpers.FromXml<string>(value);

            Assert.NotNull(order);

            Assert.Equal(value, order);

            Assert.Same(value, order);

            var castedOrder = (string)XmlHelpers.FromXml(value, typeof(string));

            Assert.NotNull(castedOrder);

            Assert.Equal(value, castedOrder);

            Assert.Same(value, castedOrder);
        }

        #endregion

        #endregion
    }

    /// <summary>
    /// Represents an order used across the tests
    /// </summary>
    public sealed class TestOrder
    {
        /// <summary>
        /// The order number
        /// </summary>
        public string OrderNumber { get; set; } = default!;
    }
}