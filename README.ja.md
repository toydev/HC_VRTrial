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
右ダブルクリックで頭の向きに合わせてビューポートを更新します。

その他の操作については通常プレイ通りキーボードとマウスで操作してください。

----

## 設定
### VRExperienceOptimization セクション
|キー|デフォルト値|説明|
|----|----|----|
|DisableLights|true|true で全てのライトを無効にします。|
|DisableLODGroups|true|true で全ての LODGroup を無効にします。|
|DisableParticleSystems|true|true で ParticleNameDisableRegex に基づき特定の ParticleSystem を無効にします。|
|ParticleNameDisableRegex|(?!Star\|Heart\|ef_ne)|無効にする ParticleSystem 名の正規表現パターンです。DisableParticleSystems を true にする必要があります。パターンにマッチする ParticleSystem を無効にします。|

### Viewport セクション
|キー|デフォルト値|説明|
|----|----|----|
|DoubleClickIntervalToUpdateViewport|0.2f|ビューポート更新のダブルクリック検出のための最大秒数です。0 以下の値で無効にできます。|
|ReflectHMDRotationXOnViewport|true|HMDの縦の向き（X 軸回転）をビューポートに反映させます。仰向けやうつ伏せで使う人は有効にしてください。|
|ReflectHMDRotationYOnViewport|true|HMDの横の向き（Y 軸回転）をビューポートに反映させます。有効にして使うのが一般的です。|
|ReflectHMDRotationZOnViewport|false|HMDの傾き（Z 軸回転）をビューポートに反映させます。横向きに寝て使う人は有効にしてください。|

以下はプレイスタイル毎の設定例です。仰向け／うつ伏せがデフォルトです。

|スタイル|ReflectHMDRotationXOnViewport<br>HMDの縦の向き<br>（X 軸回転）|ReflectHMDRotationYOnViewport<br>HMDの横の向き<br>（Y 軸回転）|ReflectHMDRotationZOnViewport<br>HMDの傾き<br>（Z 軸回転）|
|----|----|----|----|
|座位／立位|無効|有効|無効|
|仰向け／うつ伏せ（デフォルト）|有効|有効|有効|
|側位|有効|有効|有効|

----

## 開発者向け

- [開発者向け wiki ホーム](https://github.com/toydev/HC_VRTrial/wiki/Home.ja) 
