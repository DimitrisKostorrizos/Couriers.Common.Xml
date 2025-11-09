# Description

This project contains the XML related implementations, used across the implementations for Courier API clients. 
Internally, the XmlSerializer and XDocument are used, in order to expose strongly typed, generic methods, that allow the 
serialization and deserialization between XML text and .Net types.

An important note is that only public .Net types are supported, and more specifically, only the public properties, 
with a public getter and a public setter.