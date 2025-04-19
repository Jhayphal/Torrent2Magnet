using MonoTorrent;

using TextCopy;

if (args.Length != 1 || !File.Exists(args[0]))
{
    WriteError("The path to the torrent file must be specified as the only parameter.");

    return;
}

var filePath = args[0];
if (!Torrent.TryLoad(filePath, out var torrent))
{
    WriteError("I can't parse the torrent file.");

    return;
}

var infoHash = torrent.InfoHashes.V1;
if (infoHash is null)
{
    WriteError("The torrent does not contain the hash data necessary to create a magnet link.");

    return;
}

var magnet = "magnet:?xt=urn:btih:" + infoHash.ToHex();

ClipboardService.SetText(magnet);

Console.WriteLine(magnet);

static void WriteError(string text)
{
    var currentColor = Console.ForegroundColor;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ForegroundColor = currentColor;
}