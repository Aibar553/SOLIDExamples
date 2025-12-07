/*public interface IFileStorage
{
    void Save(string path, byte[] data);
    byte[] Load(string path);
    void SyncToCloud();
    string GetCloudStatus();
}

public class LocalFileStorage : IFileStorage
{
    public void Save(string path, byte[] data)
        => Console.WriteLine($"Saving to {path}");

    public byte[] Load(string path)
    {
        Console.WriteLine($"Loading from {path}");
        return Array.Empty<byte>();
    }

    public void SyncToCloud()
    {
        // ❌ Локальное хранилище само по себе не синхронизирует в облако
        throw new NotImplementedException();
    }

    public string GetCloudStatus()
    {
        // ❌ Нет облака
        throw new NotImplementedException();
    }
}

public class CloudFileStorage : IFileStorage
{
    public void Save(string path, byte[] data)
        => Console.WriteLine($"Saving to cloud {path}");

    public byte[] Load(string path)
    {
        Console.WriteLine($"Loading from cloud {path}");
        return Array.Empty<byte>();
    }

    public void SyncToCloud()
        => Console.WriteLine("Already in cloud, nothing to sync");

    public string GetCloudStatus()
        => "Online";
}*/

public interface ISave
{
    void Save(string path, byte[] data);
}
public interface ILoad
{
    byte[] Load(string path);
}
public interface ISyncToCloud
{
    void SyncToCloud();
}
public interface IGetCloudStatus
{
    string GetCloudStatus();
}
public class LocalFileStorage : ISave, ILoad
{
    public void Save(string path, byte[] data)
        => Console.WriteLine($"Saving to {path}");

    public byte[] Load(string path)
    {
        Console.WriteLine($"Loading from {path}");
        return Array.Empty<byte>();
    }
}
public class CloudFileStorage : ISave, ILoad, ISyncToCloud, IGetCloudStatus
{
    public void Save(string path, byte[] data)
        => Console.WriteLine($"Saving to cloud {path}");

    public byte[] Load(string path)
    {
        Console.WriteLine($"Loading from cloud {path}");
        return Array.Empty<byte>();
    }

    public void SyncToCloud()
        => Console.WriteLine("Already in cloud, nothing to sync");

    public string GetCloudStatus()
        => "Online";
}

/*public interface IDataExporter
{
    void ExportToCsv(string data);
    void ExportToPdf(string data);
    void ExportToXml(string data);
}

public class CsvExporter : IDataExporter
{
    public void ExportToCsv(string data)
        => Console.WriteLine("Exporting to CSV");

    public void ExportToPdf(string data)
        => throw new NotImplementedException();   // ❌

    public void ExportToXml(string data)
        => throw new NotImplementedException();   // ❌
}

public class PdfExporter : IDataExporter
{
    public void ExportToCsv(string data)
        => throw new NotImplementedException();   // ❌

    public void ExportToPdf(string data)
        => Console.WriteLine("Exporting to PDF");

    public void ExportToXml(string data)
        => throw new NotImplementedException();   // ❌
}
*/

public interface IExportToCsv
{
    void ExportToCsv(string data);
}
public interface IExportToPdf
{
    void ExportToPdf(string data);
}
public interface IExportToXml
{
    void ExportToXml(string data);
}
public class CsvExporter : IExportToCsv
{
    public void ExportToCsv(string data)
        => Console.WriteLine("Exporting to CSV");
}
public class PdfExporter : IExportToPdf
{
    public void ExportToPdf(string data)
        => Console.WriteLine("Exporting to PDF");
}