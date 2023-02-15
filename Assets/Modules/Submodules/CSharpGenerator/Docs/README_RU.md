# Create Menu Context

Данный проект поможет вам быстро создавать файлы с кодом нужного вам шаблона. Вы можете настроить какие пространства имен по умолчнию подключать в файлы кода а также нужно ли генерировать пространство имен в зависимости от иерархии папок.

Имеются быстрые меню для следующих типов:
- `классы`
- `структуры`
- `интерфейсы`
- `перечисления`
- `скрипты`
- `ScriptableObject`

Чтобы воспользоваться нужным шаблоном используйте меню `Assets` > `Create`, или нажмите правой кнопкой мыши во вкладке проекта, а затем наведите на меню `Create`.

![image](https://user-images.githubusercontent.com/5365111/200543731-31f671be-95ce-4440-97a4-0cf27a0a20f5.png)

Вы можете настроить подключаемые пространства имен и их генерацию в файлах кода, для этого вызовите меню `Windows` > `C# Generator` > `Settings`.

![image](https://user-images.githubusercontent.com/5365111/200572355-a7a55c1a-013a-42c1-818f-7ae653af6709.png)

В появившемся окне вы увидите настройки генерации пространств имен.

![image](https://user-images.githubusercontent.com/5365111/200572458-f158daec-6c00-4de2-a9e4-26e8711a38f7.png)

- `Generate Namespaces` - отвечает за генерацию пространства имен при создании файла кода. Сгенерированное пространство имен зависит от расположения файла в иерархии.
  - Если в пути расположения файла кода присутствует папка `Scripts` то она будет считаться точкой начала для генерации пространства имен, однако вместо папки `Scripts` в пространство имен будет вставлено название проекта.
  - В противном случае точкой начала пути будет считаться папка `Assets`, однако вместо папки `Assets` в пространство имен будет вставлено название проекта.
- `Default Usings` - пространства имен подключаемые по умолчанию в любой файл кода.

> Название проекта берется из `Project Settings` > `Player`> `Product Name`.

### Пример генерации пространства имен
Создайте папку `Scripts` > `Vehicles`, а в ней с помощью меню `Create` > `C# MonoBehaviour` файлы кода `Car1` и `Car2`. При этом, когда вы создаете `Car1`, то пусть опция `Generate Namespaces` в настройках `Windows` > `Create Menu Context` > `Settings` будет включена, а для `Car2` - выключена.

![image](https://user-images.githubusercontent.com/5365111/200547315-8bf04464-09ea-45be-b0c3-32845fa846da.png)

В результате в `Car1` будет сгенерирован следующий код:
```C#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

namespace MyGame.Vehicles
{
	public class Car1 : MonoBehaviour
	{
	}
}
```

А в `Car2` такой:
```C#
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityRandom = UnityEngine.Random;

public class Car2 : MonoBehaviour
{
}
```

Генерация пространства имен по папкам позволяет держать архитектуру кода чистой, вам не надо придумывать имена для пространств имен, вместо этого, придумывайте правильные названия папок. В будущем, когда ваш проект разростется до гигантских размеров, вам будет проще ориентироваться в нем, так как имена папок и пространств имен в файлах кода будут совпадать и иметь осмысленные названия.

Удачи!