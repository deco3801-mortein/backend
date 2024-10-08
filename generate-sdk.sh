#!/usr/bin/env bash
#
# Generates the SDK

sudo apt install xq

# Single-source the SDK version from `Mortein.csproj`
version=$(xq Mortein/Mortein.csproj --xpath "/Project/PropertyGroup/FileVersion")

# Initialise the mortein-sdk npm package
rm -rf mortein-sdk && mkdir mortein-sdk
cd mortein-sdk
npm init --yes --init-version "${version}" --scope="@deco3801-mortein"
jq '.files = ["**/*.d.ts", "**/*.d.ts.map", "**/*.js"]' package.json > tmp && mv tmp package.json
npx tsc --init --declaration --declarationMap --target esnext
cd ..

# Generate the OpenAPI JSON and the SDK
dotnet build Mortein
dotnet swagger tofile --output openapi.json --host https://api.vibegrow.pro Mortein/bin/Debug/net8.0/Mortein.dll openapi
npx openapi-ts

# Remove bulk exports from SDK's `index.ts` to prevent exposing multiple members with the same name.
>| mortein-sdk/index.ts
echo "Cleared mortein-sdk/index.ts"

cd mortein-sdk
npx tsc

mkdir dist --parents
npm pack --pack-destination dist
