Imports System.Configuration


Public Class ScanForm

    'CREATE TABLE InputData (
    '    id     INTEGER     PRIMARY KEY AUTOINCREMENT,
    '    crdate DATETIME,
    '    txt    TEXT (2048),
    '    split  TEXT (2048) 
    ');
    '<add name="SDBconnectionString" connectionString="metadata=res://*/SDB.csdl|res://*/SDB.ssdl|res://*/SDB.msl;provider=System.Data.SQLite.EF6;provider connection string=&quot;data source=G:\Projects\ZebraScanner\Barcoder\Barcoder\ScannerDB.db&quot;" providerName="System.Data.EntityClient" />

    Private Sub ScanForm_Load(sender As Object, e As EventArgs) Handles Me.Load

        InputTextBox.Select(0, 0)
        InputTextBox.Focus()
    End Sub

    Public Comment As String
    Public Type As Integer

    Public IsConfirm As Boolean = False

    Private Sub OkButton_Click(sender As Object, e As EventArgs) Handles OkButton.Click
        If InputTextBox.Text <> "" Then
            Try
                Dim X As New CommentForm
                X.ShowDialog()
                '
                Dim WordsPosition As String = GetWordsPosition(InputTextBox.Text & " ")
                Dim WordsPosStr() As String = WordsPosition.Split(";")
                Dim WordsPosInt As New List(Of Integer)
                WordsPosStr.AsEnumerable.ToList.ForEach(Sub(z) If IsNumeric(z) Then WordsPosInt.Add(CInt(z)))
                '
                Dim Fmt As Format
                Dim NewData = New InputData With {
                    .txt = InputTextBox.Text,
                    .crdate = Now,
                    .type = Type,
                    .comment = Comment}
                If Type = 4 Then '(unspecified)
                    NewData.split = IIf(Not String.IsNullOrEmpty(InputTextBox.Text), WordsPosition, "")
                Else
                    Fmt = StarForm_Instance.db1.Formats.Where(Function(z) z.Type = Type).FirstOrDefault
                    Dim FieldsFormat As New List(Of Integer)
                    For I As Integer = 0 To 14
                        Select Case I
                            Case 0 : FieldsFormat.Add(Fmt.F1)
                            Case 1 : FieldsFormat.Add(Fmt.F2)
                            Case 2 : FieldsFormat.Add(Fmt.F3)
                            Case 3 : FieldsFormat.Add(Fmt.F4)
                            Case 4 : FieldsFormat.Add(Fmt.F5)
                            Case 5 : FieldsFormat.Add(Fmt.F6)
                            Case 6 : FieldsFormat.Add(Fmt.F7)
                            Case 7 : FieldsFormat.Add(Fmt.F8)
                            Case 8 : FieldsFormat.Add(Fmt.F9)
                            Case 9 : FieldsFormat.Add(Fmt.F10)
                            Case 10 : FieldsFormat.Add(Fmt.F11)
                            Case 11 : FieldsFormat.Add(Fmt.F12)
                            Case 12 : FieldsFormat.Add(Fmt.F13)
                            Case 13 : FieldsFormat.Add(Fmt.F14)
                            Case 14 : FieldsFormat.Add(Fmt.F15)
                        End Select
                    Next
                    Dim LastIndex = 0
                    For I As Integer = 14 To 0 Step -1
                        If FieldsFormat(I) > 0 Then
                            LastIndex = I
                            Exit For
                        End If
                    Next
                    Dim FieldsFormatPr As String = ""
                    For I As Integer = 0 To LastIndex
                        FieldsFormatPr &= FieldsFormat(I) & ";"
                    Next
                    Debug.Print($"WordsPosition={WordsPosition} FieldsFormat={FieldsFormatPr}")
                    '
                    Dim Str1 As New Text.StringBuilder
                    If Fmt.FormatType = 0 Then
                        'by world
                        Dim CurWordsPosIntIndex As Integer = 0
                        Str1.Append(WordsPosInt(0) & ";")
                        For I As Integer = 0 To LastIndex
                            CurWordsPosIntIndex += FieldsFormat(I)
                            If CurWordsPosIntIndex < WordsPosInt.Count Then
                                Debug.Print(WordsPosInt(CurWordsPosIntIndex))
                                If FieldsFormat(I) > 0 Then
                                    Str1.Append(WordsPosInt(CurWordsPosIntIndex) & ";")
                                Else
                                    Str1.Append(";")
                                End If
                            End If
                        Next
                        Debug.Print($"Result={Str1.ToString}")
                        NewData.split = Str1.ToString
                    Else
                        'by pos
                        NewData.split = FieldsFormatPr
                    End If
                End If
                '
                Dim Y As New ConfirmForm
                Y.ParentForm = Me
                Y.Type = NewData.type
                Y.Comment = NewData.comment
                Y.CrDate = NewData.crdate
                Y.InputData = NewData.txt
                Y.SplitFormat = NewData.split
                Y.FmtRecord = fmt
                Y.ShowDialog()
                '
                If IsConfirm Then
                    StarForm_Instance.db1.InputDatas.Add(NewData)
                    StarForm_Instance.db1.SaveChanges()
                    ToolStripStatusLabel1.Text = NewData.id
                End If

                InputTextBox.Text = ""
                InputTextBox.Select(0, 0)
                InputTextBox.Focus()
            Catch ex As Exception
                MsgBox(ex.Message & vbCrLf & StarForm_Instance.CurConnectionString, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
End Class


'    <DbProviderFactories>
'      <remove invariant="System.Data.SQLite.EF6" />
'      <add name="SQLite Data Provider (Entity Framework 6)" invariant="System.Data.SQLite.EF6" description=".NET Framework Data Provider for SQLite (Entity Framework 6)" type="System.Data.SQLite.EF6.SQLiteProviderFactory, System.Data.SQLite.EF6" />
'      <remove invariant="System.Data.SQLite" />
'      <add name="SQLite Data Provider" invariant="System.Data.SQLite" description=".NET Framework Data Provider for SQLite" type="System.Data.SQLite.SQLiteFactory, System.Data.SQLite, Version=1.0.113.0, Culture=neutral, PublicKeyToken=db937bc2d44ff139" />
'    </DbProviderFactories>



'   <?xml version="1.0" encoding="utf-8"?>  '  + 
'   <edmx:Edmx Version="3.0" xmlns:edmx="http://schemas.microsoft.com/ado/2009/11/edmx">  '  + 
'     <!-- EF Runtime content -->  '  + 
'     <edmx:Runtime>  '  + 
'       <!-- SSDL content -->  '  + 
'       <edmx:StorageModels>  '  + 
'       <Schema Namespace="Model.Store" Provider="System.Data.SQLite.EF6" ProviderManifestToken="data source=G:\Projects\ZebraScanner\Barcoder\Barcoder\ScannerDB.db" Alias="Self" xmlns:store="http://schemas.microsoft.com/ado/2007/12/edm/EntityStoreSchemaGenerator" xmlns:customannotation="http://schemas.microsoft.com/ado/2013/11/edm/customannotation" xmlns="http://schemas.microsoft.com/ado/2009/11/edm/ssdl">  '  + 
'           <EntityType Name="InputData">  '  + 
'             <Key>  '  + 
'               <PropertyRef Name="id" />  '  + 
'             </Key>  '  + 
'             <Property Name="id" Type="integer" StoreGeneratedPattern="Identity" Nullable="false" />  '  + 
'             <Property Name="txt" Type="nvarchar" MaxLength="2048" />  '  + 
'             <Property Name="split" Type="nvarchar" MaxLength="2048" />  '  + 
'           </EntityType>  '  + 
'           <EntityContainer Name="ModelStoreContainer">  '  + 
'             <EntitySet Name="InputData" EntityType="Self.InputData" store:Type="Tables" />  '  + 
'           </EntityContainer>  '  + 
'         </Schema></edmx:StorageModels>  '  + 
'       <!-- CSDL content -->  '  + 
'       <edmx:ConceptualModels>  '  + 
'         <Schema Namespace="Model" Alias="Self" annotation:UseStrongSpatialTypes="false" xmlns:annotation="http://schemas.microsoft.com/ado/2009/02/edm/annotation" xmlns:customannotation="http://schemas.microsoft.com/ado/2013/11/edm/customannotation" xmlns="http://schemas.microsoft.com/ado/2009/11/edm">  '  + 
'           <EntityContainer Name="Sdb" annotation:LazyLoadingEnabled="true">  '  + 
'             <EntitySet Name="InputDatas" EntityType="Model.InputData" />  '  + 
'           </EntityContainer>  '  + 
'           <EntityType Name="InputData">  '  + 
'             <Key>  '  + 
'               <PropertyRef Name="id" />  '  + 
'             </Key>  '  + 
'             <Property Name="id" Type="Int64" Nullable="false" annotation:StoreGeneratedPattern="Identity" />  '  + 
'             <Property Name="txt" Type="String" MaxLength="2048" FixedLength="false" Unicode="true" />  '  + 
'             <Property Name="split" Type="String" MaxLength="2048" FixedLength="false" Unicode="true" />  '  + 
'           </EntityType>  '  + 
'         </Schema>  '  + 
'       </edmx:ConceptualModels>  '  + 
'       <!-- C-S mapping content -->  '  + 
'       <edmx:Mappings>  '  + 
'         <Mapping Space="C-S" xmlns="http://schemas.microsoft.com/ado/2009/11/mapping/cs">  '  + 
'           <EntityContainerMapping StorageEntityContainer="ModelStoreContainer" CdmEntityContainer="Sdb">  '  + 
'             <EntitySetMapping Name="InputDatas">  '  + 
'               <EntityTypeMapping TypeName="Model.InputData">  '  + 
'                 <MappingFragment StoreEntitySet="InputData">  '  + 
'                   <ScalarProperty Name="split" ColumnName="split" />  '  + 
'                   <ScalarProperty Name="txt" ColumnName="txt" />  '  + 
'                   <ScalarProperty Name="id" ColumnName="id" />  '  + 
'                 </MappingFragment>  '  + 
'               </EntityTypeMapping>  '  + 
'             </EntitySetMapping>  '  + 
'           </EntityContainerMapping>  '  + 
'         </Mapping>  '  + 
'       </edmx:Mappings>  '  + 
'     </edmx:Runtime>  '  + 
'     <!-- EF Designer content (DO NOT EDIT MANUALLY BELOW HERE) -->  '  + 
'     <Designer xmlns="http://schemas.microsoft.com/ado/2009/11/edmx">  '  + 
'       <Connection>  '  + 
'         <DesignerInfoPropertySet>  '  + 
'           <DesignerProperty Name="MetadataArtifactProcessing" Value="EmbedInOutputAssembly" />  '  + 
'         </DesignerInfoPropertySet>  '  + 
'       </Connection>  '  + 
'       <Options>  '  + 
'         <DesignerInfoPropertySet>  '  + 
'           <DesignerProperty Name="ValidateOnBuild" Value="true" />  '  + 
'           <DesignerProperty Name="EnablePluralization" Value="true" />  '  + 
'           <DesignerProperty Name="IncludeForeignKeysInModel" Value="true" />  '  + 
'           <DesignerProperty Name="UseLegacyProvider" Value="false" />  '  + 
'           <DesignerProperty Name="CodeGenerationStrategy" Value="None" />  '  + 
'         </DesignerInfoPropertySet>  '  + 
'       </Options>  '  + 
'       <!-- Diagram content (shape and connector positions) -->  '  + 
'       <Diagrams></Diagrams>  '  + 
'     </Designer>  '  + 
'   </edmx:Edmx>  '  + 
'    ' ; 