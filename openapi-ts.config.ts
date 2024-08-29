import { defineConfig } from "@hey-api/openapi-ts";

export default defineConfig({
    client: "fetch",
    input: "./openapi.json",
    output: {
        format: "prettier",
        path: "mortein-sdk",
    },
    services: {
        asClass: true,
        name: "{{name}}",
        operationId: false,
    },
    types: {
        dates: true,
    },
});
