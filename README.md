# Introduction
This repository contains how ReactJs and ASP.NET Core 6.0 can be used to make a tool that allows you to monitor ehat people are saying about a few selected Nigerian presidential candidates on Twitter. The purpose of this is to see if Twitter opinions really matter when it comes to the Nigerian elections.

# Installation
[Download and install the .NET Core SDK](https://dotnet.microsoft.com/download)
    * If you don't have `localdb` available on your system, [Download and install SQL Server Express](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
<p>Clone the repository</p>
<pre>
<code>git clone https://github.com/Flappizy/Politicians-Twitter-Sentiments.git</code></pre>

<p>Change directory</p>
<pre>
<code>cd Politicians-Twitter-Sentiments</code></pre>

<p>Restore all nuget packages</p>
<pre>
<code>dotnet restore </code></pre>

<p>Install node modules & run builds</p>
<pre>
<code>cd TwitterCandidateSentiments/ClientApp</code></pre>

<p>Install node modules</p>
<pre>
<code>npm install</code></pre>

<p>Change directory</p>
<pre>
<code>cd Politicians-Twitter-Sentiments/TwitterCandidateSentiments/appsettings.json</code></pre>
<p>Put your Twitter API Key/secrets in the TwitterSetting section of the file</p>
<p>Put in your connection string</p>


# Running instructions
<p>Change directory</p>
<pre>
<code>cd Politicians-Twitter-Sentiments</code></pre>
<code>dotnet run</code></pre>

# Testing instructions
<p>Change directory</p>
<pre>
<code>cd Politicians-Twitter-Sentiments/UnitTestsXunit</code></pre>
<pre>
<code>dotnet test</code></pre>

