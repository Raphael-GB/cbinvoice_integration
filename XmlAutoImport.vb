﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Ce code a été généré par un outil.
'     Version du runtime :2.0.50727.3655
'
'     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
'     le code est régénéré.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System.Xml.Serialization

'


'Ce code source a été automatiquement généré par xsd, Version=2.0.50727.3038.
'

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Public Class ImportSession

    Private batchesField() As Batch

    Private userIDField As String

    Private passwordField As String

    Private deleteBatchOnErrorField As ImportSessionDeleteBatchOnError

    Private logFileNameField As String

    Private lastErrorCodeField As String

    Private lastErrorMessageField As String

    Public Sub New()
        MyBase.New()
        Me.deleteBatchOnErrorField = ImportSessionDeleteBatchOnError.Item1
        Me.logFileNameField = "c:\ACXMLAIL.txt"
    End Sub

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Batches() As Batch()
        Get
            Return Me.batchesField
        End Get
        Set(value As Batch())
            Me.batchesField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property UserID() As String
        Get
            Return Me.userIDField
        End Get
        Set(value As String)
            Me.userIDField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Password() As String
        Get
            Return Me.passwordField
        End Get
        Set(value As String)
            Me.passwordField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute(ImportSessionDeleteBatchOnError.Item1)> _
    Public Property DeleteBatchOnError() As ImportSessionDeleteBatchOnError
        Get
            Return Me.deleteBatchOnErrorField
        End Get
        Set(value As ImportSessionDeleteBatchOnError)
            Me.deleteBatchOnErrorField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute("c:\ACXMLAIL.txt")> _
    Public Property LogFileName() As String
        Get
            Return Me.logFileNameField
        End Get
        Set(value As String)
            Me.logFileNameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property LastErrorCode() As String
        Get
            Return Me.lastErrorCodeField
        End Get
        Set(value As String)
            Me.lastErrorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property LastErrorMessage() As String
        Get
            Return Me.lastErrorMessageField
        End Get
        Set(value As String)
            Me.lastErrorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Batch

    Private batchFieldsField() As BatchField

    Private expectedBatchTotalsField() As ExpectedBatchTotal

    Private foldersField() As Folder

    Private documentsField() As Document

    Private pagesField() As Page

    Private nameField As String

    Private batchClassNameField As String

    Private descriptionField As String

    Private priorityField As BatchPriority

    Private enableAutomaticSeparationAndFormIDField As BatchEnableAutomaticSeparationAndFormID

    Private enableSingleDocProcessingField As BatchEnableSingleDocProcessing

    Private processedField As BatchProcessed

    Private relativeImageFilePathField As String

    Private errorCodeField As String

    Private errorMessageField As String

    Public Sub New()
        MyBase.New()
        Me.nameField = ""
        Me.priorityField = BatchPriority.Item5
        Me.enableAutomaticSeparationAndFormIDField = BatchEnableAutomaticSeparationAndFormID.Item0
        Me.enableSingleDocProcessingField = BatchEnableSingleDocProcessing.Item0
        Me.processedField = BatchProcessed.Item0
    End Sub

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property BatchFields() As BatchField()
        Get
            Return Me.batchFieldsField
        End Get
        Set(value As BatchField())
            Me.batchFieldsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property ExpectedBatchTotals() As ExpectedBatchTotal()
        Get
            Return Me.expectedBatchTotalsField
        End Get
        Set(value As ExpectedBatchTotal())
            Me.expectedBatchTotalsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Folders() As Folder()
        Get
            Return Me.foldersField
        End Get
        Set(value As Folder())
            Me.foldersField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Documents() As Document()
        Get
            Return Me.documentsField
        End Get
        Set(value As Document())
            Me.documentsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Pages() As Page()
        Get
            Return Me.pagesField
        End Get
        Set(value As Page())
            Me.pagesField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute("")> _
    Public Property Name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property BatchClassName() As String
        Get
            Return Me.batchClassNameField
        End Get
        Set(value As String)
            Me.batchClassNameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Description() As String
        Get
            Return Me.descriptionField
        End Get
        Set(value As String)
            Me.descriptionField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute(BatchPriority.Item5)> _
    Public Property Priority() As BatchPriority
        Get
            Return Me.priorityField
        End Get
        Set(value As BatchPriority)
            Me.priorityField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute(BatchEnableAutomaticSeparationAndFormID.Item0)> _
    Public Property EnableAutomaticSeparationAndFormID() As BatchEnableAutomaticSeparationAndFormID
        Get
            Return Me.enableAutomaticSeparationAndFormIDField
        End Get
        Set(value As BatchEnableAutomaticSeparationAndFormID)
            Me.enableAutomaticSeparationAndFormIDField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute(BatchEnableSingleDocProcessing.Item0)> _
    Public Property EnableSingleDocProcessing() As BatchEnableSingleDocProcessing
        Get
            Return Me.enableSingleDocProcessingField
        End Get
        Set(value As BatchEnableSingleDocProcessing)
            Me.enableSingleDocProcessingField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute(BatchProcessed.Item0)> _
    Public Property Processed() As BatchProcessed
        Get
            Return Me.processedField
        End Get
        Set(value As BatchProcessed)
            Me.processedField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property RelativeImageFilePath() As String
        Get
            Return Me.relativeImageFilePathField
        End Get
        Set(value As String)
            Me.relativeImageFilePathField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class BatchField

    Private nameField As String

    Private valueField As String

    Private errorCodeField As String

    Private errorMessageField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class ExpectedBatchTotal

    Private nameField As String

    Private valueField As String

    Private errorCodeField As String

    Private errorMessageField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Folder

    Private indexFieldsField() As IndexField

    Private documentsField() As Document

    Private foldersField() As Folder

    Private folderClassNameField As String

    Private errorCodeField As String

    Private errorMessageField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property IndexFields() As IndexField()
        Get
            Return Me.indexFieldsField
        End Get
        Set(value As IndexField())
            Me.indexFieldsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Documents() As Document()
        Get
            Return Me.documentsField
        End Get
        Set(value As Document())
            Me.documentsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Folders() As Folder()
        Get
            Return Me.foldersField
        End Get
        Set(value As Folder())
            Me.foldersField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property FolderClassName() As String
        Get
            Return Me.folderClassNameField
        End Get
        Set(value As String)
            Me.folderClassNameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class IndexField

    Private nameField As String

    Private valueField As String

    Private errorCodeField As String

    Private errorMessageField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Value() As String
        Get
            Return Me.valueField
        End Get
        Set(value As String)
            Me.valueField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Document

    Private indexFieldsField() As IndexField

    Private pagesField() As Page

    Private tablesField() As Table

    Private formTypeNameField As String

    Private errorCodeField As String

    Private errorMessageField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property IndexFields() As IndexField()
        Get
            Return Me.indexFieldsField
        End Get
        Set(value As IndexField())
            Me.indexFieldsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Pages() As Page()
        Get
            Return Me.pagesField
        End Get
        Set(value As Page())
            Me.pagesField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property Tables() As Table()
        Get
            Return Me.tablesField
        End Get
        Set(value As Table())
            Me.tablesField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property FormTypeName() As String
        Get
            Return Me.formTypeNameField
        End Get
        Set(value As String)
            Me.formTypeNameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Page

    Private importFileNameField As String

    Private originalFileNameField As String

    Private errorCodeField As String

    Private errorMessageField As String

    Private contentDispositionField As String

    Public Sub New()
        MyBase.New()
        Me.originalFileNameField = ""
    End Sub

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ImportFileName() As String
        Get
            Return Me.importFileNameField
        End Get
        Set(value As String)
            Me.importFileNameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute(), _
     System.ComponentModel.DefaultValueAttribute("")> _
    Public Property OriginalFileName() As String
        Get
            Return Me.originalFileNameField
        End Get
        Set(value As String)
            Me.originalFileNameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ContentDisposition() As String
        Get
            Return Me.contentDispositionField
        End Get
        Set(value As String)
            Me.contentDispositionField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Table

    Private tableRowsField() As TableRow

    Private nameField As String

    Private errorCodeField As String

    Private errorMessageField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property TableRows() As TableRow()
        Get
            Return Me.tableRowsField
        End Get
        Set(value As TableRow())
            Me.tableRowsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property Name() As String
        Get
            Return Me.nameField
        End Get
        Set(value As String)
            Me.nameField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class TableRow

    Private indexFieldsField() As IndexField

    Private errorCodeField As String

    Private errorMessageField As String

    '''<remarks/>
    <System.Xml.Serialization.XmlArrayItemAttribute(IsNullable:=False)> _
    Public Property IndexFields() As IndexField()
        Get
            Return Me.indexFieldsField
        End Get
        Set(value As IndexField())
            Me.indexFieldsField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorCode() As String
        Get
            Return Me.errorCodeField
        End Get
        Set(value As String)
            Me.errorCodeField = value
        End Set
    End Property

    '''<remarks/>
    <System.Xml.Serialization.XmlAttributeAttribute()> _
    Public Property ErrorMessage() As String
        Get
            Return Me.errorMessageField
        End Get
        Set(value As String)
            Me.errorMessageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Public Enum BatchPriority

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("7")> _
    Item7

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("8")> _
    Item8

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("9")> _
    Item9

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("1")> _
    Item1

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("2")> _
    Item2

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("10")> _
    Item10

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("3")> _
    Item3

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("4")> _
    Item4

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("5")> _
    Item5

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("6")> _
    Item6
End Enum

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Public Enum BatchEnableAutomaticSeparationAndFormID

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("0")> _
    Item0

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("1")> _
    Item1
End Enum

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Public Enum BatchEnableSingleDocProcessing

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("0")> _
    Item0

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("1")> _
    Item1
End Enum

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Public Enum BatchProcessed

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("0")> _
    Item0

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("1")> _
    Item1
End Enum

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Xml.Serialization.XmlTypeAttribute(AnonymousType:=True)> _
Public Enum ImportSessionDeleteBatchOnError

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("0")> _
    Item0

    '''<remarks/>
    <System.Xml.Serialization.XmlEnumAttribute("1")> _
    Item1
End Enum

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Batches

    Private batchField() As Batch

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Batch")> _
    Public Property Batch() As Batch()
        Get
            Return Me.batchField
        End Get
        Set(value As Batch())
            Me.batchField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class BatchFields

    Private batchFieldField() As BatchField

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("BatchField")> _
    Public Property BatchField() As BatchField()
        Get
            Return Me.batchFieldField
        End Get
        Set(value As BatchField())
            Me.batchFieldField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class ExpectedBatchTotals

    Private expectedBatchTotalField() As ExpectedBatchTotal

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("ExpectedBatchTotal")> _
    Public Property ExpectedBatchTotal() As ExpectedBatchTotal()
        Get
            Return Me.expectedBatchTotalField
        End Get
        Set(value As ExpectedBatchTotal())
            Me.expectedBatchTotalField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Folders

    Private folderField() As Folder

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Folder")> _
    Public Property Folder() As Folder()
        Get
            Return Me.folderField
        End Get
        Set(value As Folder())
            Me.folderField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Documents

    Private documentField() As Document

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Document")> _
    Public Property Document() As Document()
        Get
            Return Me.documentField
        End Get
        Set(value As Document())
            Me.documentField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class IndexFields

    Private indexFieldField() As IndexField

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("IndexField")> _
    Public Property IndexField() As IndexField()
        Get
            Return Me.indexFieldField
        End Get
        Set(value As IndexField())
            Me.indexFieldField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Pages

    Private pageField() As Page

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Page")> _
    Public Property Page() As Page()
        Get
            Return Me.pageField
        End Get
        Set(value As Page())
            Me.pageField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class Tables

    Private tableField() As Table

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("Table")> _
    Public Property Table() As Table()
        Get
            Return Me.tableField
        End Get
        Set(value As Table())
            Me.tableField = value
        End Set
    End Property
End Class

'''<remarks/>
<System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038"), _
 System.SerializableAttribute(), _
 System.Diagnostics.DebuggerStepThroughAttribute(), _
 System.ComponentModel.DesignerCategoryAttribute("code"), _
 System.Xml.Serialization.XmlRootAttribute([Namespace]:="", IsNullable:=False)> _
Partial Public Class TableRows

    Private tableRowField() As TableRow

    '''<remarks/>
    <System.Xml.Serialization.XmlElementAttribute("TableRow")> _
    Public Property TableRow() As TableRow()
        Get
            Return Me.tableRowField
        End Get
        Set(value As TableRow())
            Me.tableRowField = value
        End Set
    End Property
End Class