Imports System.Reflection

Class Application

    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Const AssemblyFile = "Samples.NW.Services.Dll"
    Const TypeName = "Bakopanos.Samples.NW.Services.Manager"

    Public Sub New()        
        Dim obj As Object = _
        Assembly.LoadFrom(AssemblyFile).CreateInstance(TypeName)
    End Sub
End Class
