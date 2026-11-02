# Dependency maintenance review — November 2026

The application was declared as `netcoreapp2.1` while already referencing `Microsoft.AspNetCore.App` 2.2.6. Restore failed with NU1202 before any new updates. Aligning the projects and module-copy paths with 2.2, using the final 2.2.8 runtime patch, and adapting `SequentialMediator.PublishCore` to MediatR 7 restores the existing application build without a framework-wide migration.

This remains an unsupported ASP.NET Core application. A successful build does not make its runtime safe for production. No deployment, database connection, email, SMS, package release, or application startup is part of this maintenance change.

## Reviewed pull requests

| PR | Proposed dependency | Decision |
|---|---|---|
| #74 | Serilog.Extensions.Logging 3.0.1 | Included; solution builds with the logging integration. |
| #93 | Bower 1.8.8 | Included, then advanced to the compatible 1.8.14 patch. |
| #99 | Microsoft.AspNetCore.App 2.2.8 | Included, with matching project targets and runtime patch. |
| #147 | cloudscribe.Web.Pagination 3.1.0 | Blocked: package requires netcoreapp3.1. |
| #161 | Autofac.Extensions.DependencyInjection 7.1.0 | Included; scoped resolution/disposal compatibility test added. |
| #163 | Twilio.AspNet.Core 5.37.2 | Included; CRM module compiles. No SMS sent. |
| #173 | ini 1.3.7 | Superseded: final lockfile has ini 1.3.8, a newer compatible patch. |
| #180 | Microsoft.VisualStudio.Web.CodeGeneration.Design 5.0.2 | Blocked: package requires net5.0. |
| #189 | y18n 3.2.2 | Included before replacing obsolete build dependencies. |
| #193 | Native Dependabot configuration | Included with its original configuration. |
| #195 | lodash 4.17.21 | Included, then advanced by the nonbreaking audit fix to 4.18.1; obsolete nested consumers removed. |
| #196 | hosted-git-info 2.8.9 | Included before replacing obsolete build dependencies. |
| #205 | Microsoft.EntityFrameworkCore.SqlServer 5.0.8 | Blocked: package requires netstandard2.1, incompatible with netcoreapp2.2. |
| #207 | MailKit 2.14.0 | Included; email module compiles. No email sent. |
| #208 | Microsoft.ApplicationInsights.AspNetCore 2.18.0 | Included; host compiles. No telemetry sent. |
| #209 | Twilio 5.64.1 | Included; CRM module compiles. No API calls made. |
| #210 | path-parse 1.0.7 | Superseded: the Gulp update already includes exactly path-parse 1.0.7. |
| #211 | Gulp 4 / minimatch | Incorporated, then moved to Gulp 5 to remove vulnerable Gulp 4 dependencies. Tasks use series/parallel and await all copy operations. |
| #212 | decode-uri-component 0.2.2 | Included before replacing obsolete build dependencies. |

The three framework-incompatible updates were independently reproduced with minimal `netcoreapp2.2` restore probes; each fails with NU1202. Their target requirements are documented in the [EF Core package](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/5.0.8), [scaffolding package](https://www.nuget.org/packages/Microsoft.VisualStudio.Web.CodeGeneration.Design/5.0.2), and [pagination package](https://www.nuget.org/packages/cloudscribe.Web.Pagination/3.1.0). Applying them requires a coordinated migration of hosting, MVC, Identity, EF/data access and modules; changing a version string alone is insufficient.

## Repeatable checks

Use the .NET 8 SDK selected by `global.json` and Node 22 or later:

```sh
dotnet build Semp.sln -c Release
dotnet test tests/Semp.Compatibility.Tests/Semp.Compatibility.Tests.csproj -c Release
cd src/Semp.WebHost
npm ci --ignore-scripts --legacy-peer-deps
npm test
npm audit
```

`--legacy-peer-deps` preserves the pre-npm-7 dependency model of the historical Angular package without installing a different Angular application. Install scripts are unnecessary for these asset tools. Neither flag disables the audit.

The .NET compatibility tests exercise the actual linked mediator implementation and the updated Autofac integration on .NET 8. They do not substitute for end-to-end execution of the unsupported 2.2 application. The asset test creates an isolated temporary module, verifies clean/copy sequencing, byte-for-byte binary preservation, views/assets, library copying, and generated JS/CSS. It never writes into application data or accesses external services.

The CI builds all projects, runs compatibility tests, and runs asset tests. It has read-only repository permissions and contains no deployment or publishing steps.

The final npm audit reports zero known vulnerabilities across the 184-package asset graph. Gulp copies preserve binary encodings explicitly and flatten the `win-x64` build output into the module loader's expected `bin` directory; tests cover both RID and portable output and Debug/Release selection. Existing NuGet/runtime vulnerabilities require further maintenance and a supported-framework migration; they are not suppressed by this change.

## Follow-up: unused email dependency

After native Dependabot resumed, #214 proposed MailKit 4.18.2. Its transitive cryptography and encoding packages warn that they do not support `netcoreapp2.2`. The active email sender uses `System.Net.Mail`; MailKit and MimeKit occurred only in unused imports and a commented-out alternative implementation. Removing that unused package and dead example eliminates MailKit/MimeKit from the dependency graph without changing the active sender or connecting to SMTP. This supersedes #214; the solution build and compatibility tests validate the removal.
