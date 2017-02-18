<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.RestartToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StartNewGameMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.QuitGAmeMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CheatModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OffToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.DisplayScoreLbl = New System.Windows.Forms.ToolStripStatusLabel()
        Me.nmbrOfStepsLbl = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Tmr1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RestartToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(982, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'RestartToolStripMenuItem
        '
        Me.RestartToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StartNewGameMenu, Me.QuitGAmeMenu})
        Me.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem"
        Me.RestartToolStripMenuItem.Size = New System.Drawing.Size(59, 24)
        Me.RestartToolStripMenuItem.Text = "game"
        '
        'StartNewGameMenu
        '
        Me.StartNewGameMenu.Name = "StartNewGameMenu"
        Me.StartNewGameMenu.Size = New System.Drawing.Size(180, 24)
        Me.StartNewGameMenu.Text = "start new game"
        '
        'QuitGAmeMenu
        '
        Me.QuitGAmeMenu.Name = "QuitGAmeMenu"
        Me.QuitGAmeMenu.Size = New System.Drawing.Size(180, 24)
        Me.QuitGAmeMenu.Text = "quit"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CheatModeToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(71, 24)
        Me.OptionsToolStripMenuItem.Text = "options"
        '
        'CheatModeToolStripMenuItem
        '
        Me.CheatModeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OnToolStripMenuItem, Me.OffToolStripMenuItem})
        Me.CheatModeToolStripMenuItem.Name = "CheatModeToolStripMenuItem"
        Me.CheatModeToolStripMenuItem.Size = New System.Drawing.Size(157, 24)
        Me.CheatModeToolStripMenuItem.Text = "cheat mode"
        '
        'OnToolStripMenuItem
        '
        Me.OnToolStripMenuItem.Name = "OnToolStripMenuItem"
        Me.OnToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.OnToolStripMenuItem.Text = "on"
        '
        'OffToolStripMenuItem
        '
        Me.OffToolStripMenuItem.Name = "OffToolStripMenuItem"
        Me.OffToolStripMenuItem.Size = New System.Drawing.Size(152, 24)
        Me.OffToolStripMenuItem.Text = "off"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DisplayScoreLbl, Me.nmbrOfStepsLbl})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 928)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(982, 25)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'DisplayScoreLbl
        '
        Me.DisplayScoreLbl.Name = "DisplayScoreLbl"
        Me.DisplayScoreLbl.Size = New System.Drawing.Size(65, 20)
        Me.DisplayScoreLbl.Text = "Score : 0"
        '
        'nmbrOfStepsLbl
        '
        Me.nmbrOfStepsLbl.Name = "nmbrOfStepsLbl"
        Me.nmbrOfStepsLbl.Size = New System.Drawing.Size(121, 20)
        Me.nmbrOfStepsLbl.Text = "remaining steps: "
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(982, 953)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Colour Smash"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents RestartToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StartNewGameMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitGAmeMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheatModeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents DisplayScoreLbl As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents nmbrOfStepsLbl As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Tmr1 As System.Windows.Forms.Timer
    Friend WithEvents OnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OffToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
