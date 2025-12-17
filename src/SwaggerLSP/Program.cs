using System;
using System.IO;
using System.Threading.Tasks;
using StreamJsonRpc;

class LspServer
{
    // Entry point
    static async Task Main(string[] args)
    {
        // Prevent Console.WriteLine from interfering with stdio JSON-RPC stream.
        // If you need logging, write to a file or use stderr.

        // ⚡ Attach JSON-RPC to stdout/stdin.
        var rpc = JsonRpc.Attach(
            sendingStream: Console.OpenStandardOutput(),
            receivingStream: Console.OpenStandardInput(),
            target: new LanguageServerRpcMethods()
        );

        // Await until the client disconnects
        await rpc.Completion.ConfigureAwait(false);
    }
}

class LanguageServerRpcMethods
{
    public OpenFile? OpenFile { get; set; }

    // Example LSP initialize request
    [JsonRpcMethod("initialize")]
    public static object Initialize(object initializeParams)
    {
        // Return a minimal initialize result
        return new
        {
            capabilities = new { }
        };
    }
    // Example custom notification
    [JsonRpcMethod("exit")]
    public static void Exit()
    {
        // LSP client tells server to exit
        Environment.Exit(0);
    }

    // You can add more methods e.g. textDocument/didOpen, etc.
    [JsonRpcMethod("textDocument/didOpen")]
    public async Task DidOpen(string path)
    {
        // handle document open
        OpenFile = new OpenFile(await File.ReadAllTextAsync(path));
    }
}

class OpenFile(string fileText)
{
    public string FileText { get; set; } = fileText;
}
