# Umbraco Headless CMS prototype

The prototype is an example of how to use Umbraco as a headless CMS for a website. The main focus or ambition of this project is to create a generic headless CMS solution based on Umbraco, that will become a reusable product and fulfills all CMS requirements for the given set of different websites being developed by the business. These websites will have a common layout structure and will be using a set of common components, but their CMS needs and requirements will likely differ greatly; some of the sites may have a greater need for adding different content pages with specialized modules for example.

## CMS endpoints

The headless CMS will be serving content through specific API endpoints for content types like pages, layouts and components.

## CMS instances

The prototype content design constitutes a multi-instance architecture for the different CMS instances we need for the different web sites; which means that we create a test and a prod CMS instance for each web site.
