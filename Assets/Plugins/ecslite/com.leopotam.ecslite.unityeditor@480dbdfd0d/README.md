# Интеграция в редактор Unity для LeoECS Lite
Интеграция в редактор Unity с мониторингом состояния мира.

> Проверено на Unity 2020.3 (зависит от Unity) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.

# Содержание
* [Социальные ресурсы](#Социальные-ресурсы)
* [Установка](#Установка)
    * [В виде unity модуля](#В-виде-unity-модуля)
    * [В виде исходников](#В-виде-исходников)
* [Интеграция](#Интеграция)
    * [Через код](#Через-код)
    * [Через меню проекта](#Через-меню-проекта)
* [Лицензия](#Лицензия)
* [ЧаВо](#ЧаВо)

# Социальные ресурсы
[![discord](https://img.shields.io/discord/404358247621853185.svg?label=enter%20to%20discord%20server&style=for-the-badge&logo=discord)](https://discord.gg/5GZVde6)


# Установка

> **ВАЖНО!** Зависит от [LeoECS Lite](https://github.com/Leopotam/ecslite) - фреймворк должен быть установлен до этого расширения.

## В виде unity модуля
Поддерживается установка в виде unity-модуля через git-ссылку в PackageManager или прямое редактирование `Packages/manifest.json`:
```
"com.leopotam.ecslite.unityeditor": "https://github.com/Leopotam/ecslite-unityeditor.git",
```
По умолчанию используется последняя релизная версия. Если требуется версия "в разработке" с актуальными изменениями - следует переключиться на ветку `develop`:
```
"com.leopotam.ecslite.unityeditor": "https://github.com/Leopotam/ecslite-unityeditor.git#develop",
```

## В виде исходников
Код так же может быть склонирован или получен в виде архива со страницы релизов.

# Интеграция

## Через код
```c#
// Инициализация окружения.

IEcsSystems _systems;

void Start () {        
    _systems = new EcsSystems (new EcsWorld ());
    _systems
        .Add (new TestSystem1 ())
#if UNITY_EDITOR
        // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
        // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
        .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
        // Регистрируем отладочные системы по контролю за текущей группой систем. 
        .Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem ())
#endif
        .Init ();
}

void Update () {
    // Отладочные системы являются обычными ECS-системами и для их корректной работы
    // требуется выполнять их запуск через EcsSystems.Run(), иначе имена сущностей
    // не будут обновляться, к тому же это приведет к постоянным внутренним аллокациям.
    _systems?.Run();
}
```

> **ВАЖНО!** По умолчанию названия компонентов записываются в имя `GameObject` на каждом изменении списка компонентов на сущности.
> Если такое поведение не нужно (например, для повышения производительности работы в редакторе), то его
> можно изменить через параметр `bakeComponentsInName = false` в конструкторе `EcsWorldDebugSystem`.


## Через меню проекта
Часть кода может быть автоматически сгенерирована через контекстное меню проекта `Assets / Create / LeoECS Lite`.

# Лицензия
Фреймворк выпускается под двумя лицензиями, [подробности тут](./LICENSE.md).

В случаях лицензирования по условиям MIT-Red не стоит расчитывать на
персональные консультации или какие-либо гарантии.

# ЧаВо

### Я выполнил интеграцию, но не могу понять, где теперь смотреть состояние мира?

Нужно перейти в режим `PlayMode` и развернуть иерархию объектов в системной сцене `DontDestroyOnLoad` - там должны появиться объекты с именами `[ECS-WORLD]` для каждого мира.
Вся иерархия сущностей внутри этого объекта является виртуальной, создается только для визуализации наличия компонентов на сущностях и не участвует в реальной работе ядра.

### У меня много сущностей и я хочу как-то отфильтровать только те, на которых есть нужные мне компоненты. Как это сделать?

По умолчанию имена типов компонентов добавляются к имени GameObject-а сущности, на которой они существуют - это можно использовать для фильтрации штатными средствами
редактора Unity (строка поиска в окне иерархии сцены) для поиска GameObject-ов, подходящих под указанный шаблон.

### Я хочу создавать сущности в `IEcsPreInitSystem`-системе, но отладочные системы бросают исключения в этом случае. Как это исправить?

Проблема в том, что `EcsWorldDebugSystem` тоже является `IEcsPreInitSystem`-системой и происходит конфликт из-за порядка систем.
Решение - все отладочные системы следует вынести в отдельный `IEcsSystems` и вызвать его инициализацию раньше основного кода:
```c#
    IEcsSystems _systems;
#if UNITY_EDITOR
    IEcsSystems _editorSystems;
#endif
    void Awake () {
        _systems = new EcsSystems (new EcsWorld ());
#if UNITY_EDITOR
        // Создаем отдельную группу для отладочных систем.
        _editorSystems = new EcsSystems (_systems.GetWorld ());
        _editorSystems
            .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
            .Init ();
#endif
        _systems
            .Add (new Sys ())
#if UNITY_EDITOR
            // Подключаем контроль систем в ту группу, которая содержит наш код.
            .Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem ())
#endif
            .Init ();
    }
    
    void Update () {
        _systems?.Run ();
#if UNITY_EDITOR
        // Выполняем обновление состояния отладочных систем. 
        _editorSystems?.Run ();
#endif
    }
    
    void OnDestroy () {
#if UNITY_EDITOR
        // Выполняем очистку отладочных систем.
        if (_editorSystems != null) {
            _editorSystems.Destroy ();
            _editorSystems = null;
        }
#endif
        if (_systems != null) {
            _systems.Destroy ();
            _systems.GetWorld ().Destroy ();
            _systems = null;
        }
    }
```

### Я хочу добавить поддержку редактирования полей моих компонентов, как это сделано для простых типов. Как я могу это сделать?

Это можно сделать через реализацию инспектора для отдельного типа поля, либо компонента целиком:
```c#
public struct C2 {
    public string Name;
}

// Файл должен лежать где-то в проекте - будет обнаружен и подключен автоматически.
#if UNITY_EDITOR
sealed class C2Inspector : EcsComponentInspectorTyped<C2> {
    public override bool OnGuiTyped (string label, ref C2 value, EcsEntityDebugView entityView) {
        EditorGUILayout.LabelField ($"Super C2 component", EditorStyles.boldLabel);
        var newValue = EditorGUILayout.TextField ("Name", value.Name);
        EditorGUILayout.HelpBox ($"Hello, {value.Name}", MessageType.Info);
        // Если значение не поменялось - возвращаем false.
        if (newValue == value.Name) { return false; }
        // Иначе - обновляем значение и возвращаем true.
        value.Name = newValue;
        return true;
    }
}
#endif
```