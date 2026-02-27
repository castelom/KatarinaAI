# KatarinaAI — Technical Product Owner Agent

## 📌 Overview

**KatarinaAI** is a Technical Product Owner agent that converts informal requirements and short feature descriptions into structured technical tickets ready for development.

The agent outputs a standardized JSON ticket containing:

- `title`
- `objective`
- `description`
- `acceptanceCriteria` (list of testable items)
- `impactedComponents` (backend components / services / controllers / repositories)
- `suggestedMethods` (implementation-level guidance)

---

## ⚙️ How It Works

1. The API exposes a **POST** endpoint that accepts a single `requirement` string.  
2. The application sends a structured prompt to a configured LLM provider (e.g., Gemini).  
3. The returned response is parsed into a `GenerateTicketResponse`.  
4. Programmatic validation and retry logic enforce a strict JSON schema.

---

## ✅ Prerequisites

- .NET 10 SDK installed  
- A Gemini / Google GenAI API key (or other configured provider key)

---

## 🔐 Configure API Key

### Option 1 — Edit `appsettings.Development.json` (Quick Local Test)

Add your API key under the `Gemini` section:

```json
{
  "Gemini": {
    "ApiKey": "YOUR_GEMINI_API_KEY",
    "Model": "gemini-2.5-flash-lite"
  }
}
```

`Program.cs` reads `Gemini:ApiKey` and sets `GOOGLE_API_KEY` for the Google GenAI client.

---

### Option 2 — Use dotnet user-secrets (Recommended for Local Development)

From the project directory:

Initialize (if not already done):

```bash
dotnet user-secrets init
```

Set the key:

```bash
dotnet user-secrets set "Gemini:ApiKey" "YOUR_GEMINI_API_KEY"
```

This keeps secrets out of source control.

---

## 🚀 Run and Test Locally

From the API project folder:

Start the application:

```bash
dotnet run
```

Swagger UI (OpenAPI) is available in Development mode:

```
https://localhost:{PORT}/swagger
```

---

### Example curl Request

```bash
curl -k -X POST "https://localhost:7136/Ticket" \
  -H "Content-Type: application/json" \
  -d "{\"requirement\":\"Allow users to reset their password via email.\"}"
```

---

### Example Request Body

```json
{
  "requirement": "Allow users to reset their password via email."
}
```

---

### Successful Response

A JSON object matching `GenerateTicketResponse`:

```json
{
  "title": "string",
  "objective": "string",
  "description": "string",
  "acceptanceCriteria": ["string"],
  "impactedComponents": ["string"],
  "suggestedMethods": ["string"]
}
```

---

## 🛠 Notes & Troubleshooting

- Ensure `Gemini:ApiKey` is present; otherwise, authentication will fail.  
- If navigating to `/` returns 404, open `/swagger` or call `/Ticket` directly.  
- Check logs and exceptions for prompt or JSON parsing errors from the LLM provider.

---

## 📂 Files of Interest

- `Program.cs` — Application wiring, Gemini options binding, and client creation  
- `appsettings.Development.json` — Local configuration (add `Gemini:ApiKey` here for quick testing)  
- `TicketController.cs` — `POST /Ticket` endpoint  
- `GenerateTicketResponse.cs` — Response schema  

---

Built to transform informal ideas into structured, production-ready technical specifications.
