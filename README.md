# azure-functions-startup-blob-storage
Azure Functions のローカルデバッグで Storage Blob を利用するサンプル（Managed ID 使用）

## テンプレートから実装した機能

- Startup クラスの追加
  - Microsoft.Azure.Functions.Extensions
  - Microsoft.Extensions.DependencyInjection
  
- 環境ごとの設定ファイル appsettings.{enviroment}.json の構成
  - [構成ソースのカスタマイズ](https://docs.microsoft.com/ja-jp/azure/azure-functions/functions-dotnet-dependency-injection#customizing-configuration-sources)

- appsettings.{enviroment}.json を appsettings.json に統合
  - [.NET Coreコンソールアプリにappsettings.jsonを追加する](https://noxi515.hateblo.jp/entry/2020/05/23/211702)

- Options パターンを用いて Storage Blob の構成情報を POCO クラスにマッピングする
  - [オプションと設定の使用](https://docs.microsoft.com/ja-jp/azure/azure-functions/functions-dotnet-dependency-injection#working-with-options-and-settings)

- Blob Storage に対して Azure SDK for .NET を使ってマネージドIDで認証する
  - Microsoft.Extensions.Azure
  - Azure.Identity
  - Azure.Storage.Blobs
  - [Azure SDK for .NET での依存関係の挿入](https://docs.microsoft.com/ja-jp/dotnet/azure/sdk/dependency-injection)

- ローカルデバッグ実行用(Azurite利用)のプロファイルと、Azure 上のリソースを利用した上でのデバッグ実行用のプロファイルを用意する

- サービスクラス（今回は Repository クラス）を DI する

## ローカルデバッグ用の環境構築(azurite)

以下の記事の通りに環境構築することで new DefaultCredential() を利用して Blob Storage をローカルデバッグ実行時に操作ができた。

- [Local Azure Storage Development with Azurite, Azure SDKs, and Azure Storage Explorer](https://blog.jongallant.com/2020/04/local-azure-storage-development-with-azurite-azuresdks-storage-explorer/)

1. c:\azurite フォルダーを作成する
2. Chocolately をインストールする
3. choco install mkcert
4. mkcert install
5. mkcert 127.0.0.1
6. choco install nodejs.install -y
7. npm install -g azurite
8. azurite --oauth basic --cert 127.0.0.1.pem --key 127.0.0.1-key.pem

- Visual Studio を先に起動するとポート被りで azurite の起動に失敗するので、先に実行しておく
- 資格情報はあらかじめ az login しておく
