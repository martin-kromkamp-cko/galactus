# Galactus

Galactus is the central Processing configuration service. It's setup in a way to act as the source of truth for merchant configuration while giving internal teams the freedom to consume this data in a way convenient to their systems.

## Authentication

Authentication on Galactus is managed trough Okta for end-users and internal Access (TODO!) for service to service communication. Given the scope of the K/V nodes they are only accessible with service to service authentication.

TODO: Describe how to use this

## Interfaces

Galactus exposes merchant configuration in four high level ways.

- [Rest API]()
- [GraphQL API]()
- [Change events]()
- [K/V nodes]()

The first two (Rest/GraphQL API) are focused on low volume flexible consumption. The latter two (change events and K/V nodes) are for high volume and low latency access.

### Rest API

The rest API is for well known, and there for optimized, access patterns. These support stable CAT features and Dashboard systems. 
Usually features start are build on top of the GraphQL API first for flexibility and are migrated to optimized Rest endpoints once the feature becomes stable.

### GraphQL API

The GraphQL API serves two main purposes, data exploration and front-end development flexibility.

Data exploration is critical for internal end-users as it unlocks to ask creative and one-off questions to the data graph without the need for support from development teams. These questions are often analytical and of a temporary nature. Introducing specialized rest endpoints isn´t desireable for that case.

Giving the front-end teams the flexibility to explore building new features without the involvement of other development teams to get the needed endpoints build first is key to decouple development lifecycle and remove friction between teams. For this reason the GraphQL is available to these systems to use. In order to protect the system from in-efficient resource usage it is more often than not desirable to promote the GraphQL queries to optimized Rest endpoints once a front-end feature is stable.

### Change events

TODO

### K/V nodes

TODO
