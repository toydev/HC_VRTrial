[English](README.md)

# VR Trial plugin for Honey Come
Honey Come の VR プラグイン実験プロジェクトです。

たくさんの課題があり不完全ですが、Honey Come VR の可能性を感じることができます。

できそうだと感じたら課題を一緒に解決しましょう。

そして、自分好みの VR プラグインの構築に挑戦しましょう。

----

## 前提
- Honey Come
- 最新バージョンの [BepInEx 6.x Unity IL2CPP for Windows (x64) games](https://builds.bepinex.dev/projects/bepinex_be)
- HMD（私は Meta Quest 2 を使っているが SteamVR が認識すれば基本的には動くと思う）
- SteamVR
- [BepisPlugins](https://github.com/IllusionMods/BepisPlugins/)
- [BepInEx.ConfigurationManager](https://github.com/BepInEx/BepInEx.ConfigurationManager)
- 他の VR プラグインが入っていないこと

----

## 遊び方
ゲームに [HC_VRTrial](https://github.com/toydev/HC_VRTrial/releases) をインストールし、HMD と SteamVR を接続してゲームを開始してください。

開始時に SteamVR のプロセスを検出して有効になります。

----

## 操作
特別な操作方法は実装していません。

通常プレイ通り、キーボード・マウスで操作してください。

----

## 設定
|セクション|キー|デフォルト値|説明|
|----|----|----|----|
|General|IsLightDisabled|true|ライトの無効化を有効にします。true でライトを無効にします。|
|General|IsLODGroupDisabled|true|LODGroup の無効化を有効にします。true でライトを無効にします。|
|General|IsParticleSystemDisabled|true|パーティクルシステムの無効化を有効にします。true でライトを無効にします。|
|General|DisabledParticleNameRegex|(?!Star\|Heart\|ef_ne)|無効化対象のパーティクルシステム名のパターンです。この正規表現にマッチする名前のパーティクルシステムが無効化されます。|

----

## 開発者向け

- [開発者向け wiki ホーム](https://github.com/toydev/HC_VRTrial/wiki/Home.ja) 
