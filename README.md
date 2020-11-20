# PricingServices
Financial Pricing API Service (Bloomberg Provider)

Welcome to Pricing API Service sample solution.
This solution allows to connect to a different pricing providers and request securities pricing info. You can request different fields like pxLast (security last market price) or company or instrument info. Depending on your subscription additional charges may be applied.

This solution includes a Net core 5 console project and some netstandard class Libraries, including a Bloomberg pricing provider

In a short future I'd like to implement other financial providers like Refinitiv, SIX and others.
You can also create your own provider for other pricing services implementing interface IPricingAPIService

## Important Note 
A BEAP (Bloomberg Enterprise Access Point) license is required in order to obtain valid credentials to connect.
You can request a test license contacting [Enterprise Access Point portal](https://www.bloomberg.com/professional/product/enterprise-access-point/)

## Sample
