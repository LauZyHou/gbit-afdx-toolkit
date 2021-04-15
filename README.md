# gbit-afdx-toolkit
⚙️千兆AFDX套件-模型配置和生成工具。

## DSL说明

### `*.gafdx.metainf`
适合于某个工具的特定模型的替换规则与解释说明文件，形如：
```
【key1】    bool    解释说明文字
【key2】    string  解释说明文字
```
第一项，“替换键”是该项的唯一标识，约定以`【】`符号包围，并互不相同。

第二项，“类型标记”用于在工具中生成配置项，只支持`bool`（生成勾选框）和`string`（生成文本框）。

第三项，“解释说明文字”将在工具中生成对应配置项前的说明文字。

## 使用说明

对于每个希望配置和生成的模型，都要在`Templates`下对应工具的目录中添加一个`*.gafdx.metainf`文件，并构建一个同名子目录放置模板文件。

例如，在`Templates/UPPAAL`下添加一个`MySpecMod.gafdx.metainf`文件，并构建`Templates/UPPAAL/MySpecMod`子目录，将该模型的所有模板放入其中。

书写`MySpecMod.gafdx.metainf`文件，例如：
```
【BAG】     string  带宽分配间隔
【Lmax】    string  最大帧长
【token】   bool    交换机令牌桶流量控制
```

在`Templates/UPPAAL/MySpecMod`子目录下的模型文件中，保证相应的参数值位置书写为对应的`【key】`。

启动工具，可以发现在左侧的`UPPAAL`选项卡下，上方具有一个名为`MySpecMod`的子选项卡面板，其中包含三个配置项，分别是：
- 带宽分配间隔 文本框
- 最大帧长 文本框
- 交换机令牌桶流量控制 勾选框

为各项设置值后，点击下方的生成模型，若下方提示生成成功，则可以在`Products/UPPAAL/MySpecMod`子目录下找到所生成的全部模型文件。由于是UPPAAL模型，因此可以将其导入到指定版本的UPPAAL工具中使用。
