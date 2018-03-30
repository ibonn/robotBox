Imports System.IO

Module Settings

    Private settings As New Dictionary(Of String, String)       ' Settings
    Private langs As New Dictionary(Of String, Language)        ' Available languages
    Private langMap As New Dictionary(Of String, String)        ' Name -> Code
    Private lang As String                                      ' Current language

    Private Structure Language
        Public name As String
        Public code As String
        Public dict As Dictionary(Of String, String)
    End Structure

    Public ReadOnly Property langName As String
        Get
            If langs.ContainsKey(lang) Then
                Return langs(lang).name
            Else
                Return "Unknown"
            End If
        End Get
    End Property

    Public ReadOnly Property langCode As String
        Get
            If langs.ContainsKey(lang) Then
                Return langs(lang).code
            Else
                Return "Unknown"
            End If
        End Get
    End Property

    Public Function getLangCode(name As String) As String
        Return langMap(name)
    End Function

    Public Sub loadSettings()
        ' Load settings
        If File.Exists("config.xml") Then
            Dim settingsDoc As XDocument = XDocument.Load("config.xml")
            For Each s As XElement In settingsDoc.Root.Elements("setting")
                settings.Add(s.@name.ToString, s.Value.ToString)
            Next

            ' Load languages
            loadLanguages()

            ' Set language
            lang = getSetting("language")
        Else
            ' Create file and call loadSettings()
            File.WriteAllText("config.xml", "<?xml version=""1.0""?>
                                             <config>
                                                <setting name=""language"">en</setting>
                                                <setting name=""autoAdjustFrameRate"">true</setting>
                                                <setting name=""maxFrameRate"">40</setting>
                                                <setting name=""minFrameRate"">1</setting>
                                                <setting name=""frameRate"">40</setting>
                                                <setting name=""defaultPort"">15000</setting>
                                                <setting name=""gridSize"">50</setting>
                                                <setting name=""startX"">37</setting>
                                                <setting name=""startY"">37</setting>
                                                <setting name=""startAngle"">0</setting>
                                             </config>")
            loadSettings()
        End If
    End Sub

    Public Function getAvailableLanguages() As String()
        Return langMap.Keys.ToArray
    End Function

    Public Sub loadLanguages()
        Dim langDoc As XDocument
        Dim l As Language
        For Each fn As String In Directory.GetFiles("lang")
            If Path.GetExtension(fn) = ".xml" Then
                l = New Language
                langDoc = XDocument.Load(fn)
                l.name = langDoc.Root.@name.ToString
                l.code = langDoc.Root.@code.ToString
                l.dict = New Dictionary(Of String, String)
                For Each e As XElement In langDoc.Root.Elements("param")
                    l.dict.Add(e.@name.ToString, e.Value.ToString)
                Next
                langs.Add(l.code, l)
                langMap.Add(l.name, l.code)
            End If
        Next
    End Sub

    Public Function getLang(param As String) As String
        If langs(lang).dict.ContainsKey(param) Then
            Return langs(lang).dict(param)
        End If
        Return Nothing
    End Function

    Public Function getSetting(name As String) As Object
        If settings.ContainsKey(name) Then
            Return settings(name)
        End If
        Return Nothing
    End Function

    Public Sub setSetting(name As String, value As Object)
        If settings.ContainsKey(name) Then
            settings(name) = value
        Else
            settings.Add(name, value)
        End If
    End Sub

    Public Sub saveSettings()
        Dim settingsDoc As New XDocument(New XElement("config"))
        For Each kvp As KeyValuePair(Of String, String) In settings
            settingsDoc.Root.Add(New XElement("setting", New XAttribute("name", kvp.Key), kvp.Value))
        Next
        settingsDoc.Save("config.xml")
    End Sub
End Module
