
' to do : - tegel moet duidelijk zichtbaar zijn wanneer geselecteerd

Option Explicit On
Option Strict On

Imports System.Threading

Public Class Form1

#Region "Declarations"
    ' Size of matrix constants

    Const MATRIX_WIDTH As Integer = 10
    Const MATRIX_HEIGHT As Integer = 10

    ' tile size constants

    Const TILE_WIDTH As Integer = 50
    Const TILE_HEIGHT As Integer = 50

    ' time interval for threads (in milliseconds)

    Const timeInterval = 2

    ' colors declarations
    Dim RED As Color = Color.FromArgb(255, 207, 0, 15)
    Dim ORANGE As Color = Color.FromArgb(255, 248, 148, 6)
    Dim YELLOW As Color = Color.FromArgb(255, 247, 202, 24)
    Dim GREEN As Color = Color.FromArgb(255, 3, 166, 120)
    Dim BLUE As Color = Color.FromArgb(255, 30, 139, 195)
    Dim PURPLE As Color = Color.FromArgb(255, 191, 85, 236)


    ' Matrix with width 10 (0 to 9) and height 10 (0 to 9)
    Private gameBoard(MATRIX_WIDTH - 1, MATRIX_HEIGHT - 1) As Label

    Private labelsSelected As Integer = 0     ' the amount of labels that are selected

    ' makes a random number generator
    Dim rnd As Random = New Random()

    Dim remainingSteps As Integer = 20

    Dim score As Integer = -1    ' is -1 to check if the game has already started, before the game removed jewels don't count
    Dim highscore As Integer = 0

    Dim cheatmode As Boolean = False ' ndicates if cheat mode is selected

    Dim checkingMoves As Boolean = False    ' indicates wether the function is just for checking if the function is called in function of movesPossible
#End Region

    ' sub starts game 
    Private Sub startGame() Handles Me.Load   'executes when form is loaded 

        'make a gameboard
        make_gameBoard()

        score = 0      ' makes it possible to get points for removed jewels

        ' create handlers for jewels
        createJewelHandler()

    End Sub

    Private Sub newGame() Handles StartNewGameMenu.Click

        emptyGameboard()
        make_gameBoard()

        ' set global variables to the right value

        labelsSelected = 0
        score = 0
        DisplayScoreLbl.Text = "score : " + CType(score, String)

        remainingSteps = 20

        ' create handlers for jewels

        createJewelHandler()

    End Sub

    Private Sub make_gameBoard()

        ' displays the amount of steps the player gets
        nmbrOfStepsLbl.Text = "remaining Steps: " + CStr(remainingSteps)

        Me.Size = New System.Drawing.Size((MATRIX_WIDTH + 4) * 50, (MATRIX_HEIGHT + 4) * 50)

        ' gives the width and the height of a gameBoard
        Dim board_width As Integer = Me.Width
        Dim board_height As Integer = Me.Height


        Dim spacingy = 0
        Dim spacingx = 0

        ' initiates gameboard
        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0)

            spacingy = 0

            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1)

                Dim jewel = New Label()

                jewel.AutoSize = False           ' makes it possible to change the size of a tile

                jewel.Size = New Size(TILE_WIDTH, TILE_HEIGHT)
                jewel.TextAlign = ContentAlignment.MiddleCenter
                jewel.Name = "tile_" & i & "_" & j

                'x, y tile location in the form 
                jewel.Location = New Point(TILE_WIDTH * (i + 1) - spacingx, TILE_HEIGHT * (j + 1) - spacingy)

                spacingy -= 2

                ' color of a tile
                jewel.BackColor = give_colour()

                ' saves the tile to gameboard
                gameBoard(i, j) = jewel

                ' gives tile a visible border

                jewel.BorderStyle = BorderStyle.FixedSingle

                If gameBoard(MATRIX_WIDTH - 1, MATRIX_HEIGHT - 1) IsNot Nothing Then      ' checks if all jewels have a value 
                    Dim wrongJewels = CheckForRow()
                    ' changes the color of the wrong tiles
                    makeTransparant(wrongJewels)

                    While wrongJewels.Count > 0

                        changeTransparantColour()

                        wrongJewels = CheckForRow()

                        makeTransparant(wrongJewels)

                    End While
                End If

                ' add controls to gameboard
                Me.Controls.Add(gameBoard(i, j))

            Next

            spacingx -= 2

        Next

    End Sub

    Sub emptyGameboard()

        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0)
            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1)

                Me.Controls.Remove(gameBoard(i, j))

            Next
        Next

    End Sub

    ' creates a handler for every jewel in the game
    Sub createJewelHandler()
        For Each cntr As Control In Me.Controls                     ' creates a handler for every label in the form
            If TypeOf cntr Is Label Then
                AddHandler cntr.MouseDown, AddressOf jewelClicked

            End If
        Next
    End Sub

    ' gives a random colour as background for a jewel
    Private Function give_colour() As Color
        Dim randomNumber = Rnd.Next(0, 6)

        Dim listColors() As Color = {RED, ORANGE, YELLOW, GREEN, BLUE, PURPLE}
        Dim tileColour As Color = listColors(randomNumber)
        Return tileColour

    End Function

    'executes when a label is clicked
    Private Sub jewelClicked(sender As Object, ByVal e As MouseEventArgs)

        'sender is clicked label
        Dim currentLabel As Label = CType(sender, Windows.Forms.Label)

        'selects or deselects a label 
        select_deselectLabel(currentLabel)

        'draws the new border around the selected label

        Me.Refresh()

        ' updates gameboard variable 
        update_gameboard(currentLabel)

        If labelsSelected > 1 Then
            ' swaps jewels 

            Dim swappedJewels = swapJewels(currentLabel)

            Thread.Sleep(30)    ' pauses the game before removing swapped jewels

            Dim removeJewelsList = CheckForRow()

            makeTransparant(removeJewelsList)

            Thread.Sleep(150)

            dropJewels()

            movesPossible()

            ' if no jewels have to be removed and you have swapped 2 jewels , than the jewels have to be unswapped

            If removeJewelsList.Count < 1 And swappedJewels IsNot Nothing And cheatmode <> True Then

                unswapJewels(swappedJewels)

            End If

        End If

        ' changes the label in the window to the amount of steps left
        nmbrOfStepsLbl.Text = "remaining Steps: " + CStr(remainingSteps)

        ' checks if the amount of steps are still valid
        checkNmbrSteps()

        'checks if there are still moves possible but is broken 

    End Sub

    Private Function CheckForRow() As List(Of Label) ' checks for rows of tiles and makes them black

        Dim removeSet As List(Of Label) = New List(Of Label)

        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0)
            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1)

                If i < gameBoard.GetUpperBound(0) - 1 Then

                    Dim firstJewel As Label = gameBoard(i, j)
                    Dim secondJewel As Label = gameBoard(i + 1, j)
                    Dim thirdJewel As Label = gameBoard(i + 2, j)

                    If firstJewel.BackColor = secondJewel.BackColor And firstJewel.BackColor = thirdJewel.BackColor Then
                        Dim selectedJewelcolor = gameBoard(i, j).BackColor
                        Dim offset As Integer = 0

                        While offset < gameBoard.GetUpperBound(0) - i And gameBoard(i + offset, j).BackColor = selectedJewelcolor

                            removeSet.Add(gameBoard(i + offset, j))
                            offset += 1

                        End While

                    End If

                ElseIf i = gameBoard.GetUpperBound(0) - 1 Then  ' checks the last jewel on the right and adds it to the array if it has to be removed

                    Dim firstJewel = gameBoard(i - 1, j)
                    Dim secondJewel = gameBoard(i, j)
                    Dim thirdJewel = gameBoard(i + 1, j)

                    If firstJewel.BackColor = secondJewel.BackColor And firstJewel.BackColor = thirdJewel.BackColor Then

                        removeSet.Add(thirdJewel)

                    End If
                End If

            Next
        Next

        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0)             ' needs to iterate the whole list again to make sure 
            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1)         'that the values of the right 2 columns are correct

                If j < gameBoard.GetUpperBound(0) - 1 Then

                    Dim firstJewel = gameBoard(i, j)
                    Dim secondJewel = gameBoard(i, j + 1)
                    Dim thirdJewel = gameBoard(i, j + 2)

                    If firstJewel.BackColor = secondJewel.BackColor And firstJewel.BackColor = thirdJewel.BackColor Then
                        Dim selectedJewelcolor = gameBoard(i, j).BackColor
                        Dim offset As Integer = 0
                        ' indicates the distance in coordinates from the selectedJewel

                        While offset < gameBoard.GetUpperBound(1) - j And gameBoard(i, j + offset).BackColor = selectedJewelcolor

                            removeSet.Add(gameBoard(i, j + offset))
                            offset += 1

                        End While

                    End If

                ElseIf j = gameBoard.GetUpperBound(0) - 1 Then          ' checks the lowest tile on the board and adds it to the array if it has to be removed

                    Dim firstJewel = gameBoard(i, j - 1)
                    Dim secondJewel = gameBoard(i, j)
                    Dim thirdJewel = gameBoard(i, j + 1)

                    If firstJewel.BackColor = secondJewel.BackColor And firstJewel.BackColor = thirdJewel.BackColor Then

                        removeSet.Add(thirdJewel)

                    End If
                End If
            Next
        Next


        If score <> -1 And checkingMoves = False Then

            score = CInt(score + (removeSet.Count ^ 2))

            DisplayScoreLbl.Text = "score: " + CType(score, String)

        End If

        Return removeSet

    End Function

    Private Sub dropJewels()

        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0)
            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1)

                Dim currentJewel = gameBoard(i, j)

                If j = 0 And currentJewel.BackColor = Color.Transparent Then

                    gameBoard(i, j).BackColor = give_colour()

                    Thread.Sleep(timeInterval)

                    Me.Refresh()

                ElseIf j <> gameBoard.GetUpperBound(1) Then


                    Dim lowerJewel = gameBoard(i, j + 1)
                    If lowerJewel.BackColor = Color.Transparent Then

                        lowerJewel.BackColor = currentJewel.BackColor
                        currentJewel.BackColor = Color.Transparent

                        Thread.Sleep(timeInterval)

                        Me.Refresh()

                    End If

                Else

                    currentJewel = gameBoard(i, j)
                    Dim upperJewel = gameBoard(i, j - 1)

                    If currentJewel.BackColor = Color.Transparent Then

                        currentJewel.BackColor = upperJewel.BackColor
                        upperJewel.BackColor = Color.Transparent

                        Thread.Sleep(timeInterval)

                        Me.Refresh()

                    End If

                End If

            Next

        Next

        Thread.Sleep(3 * timeInterval)    'Waits before it starts reoving new combinations

        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0)             ' checks if board if no new combinations have formed and when it has removes them
            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1)

                If gameBoard(i, j).BackColor = Color.Transparent Then

                    dropJewels()
                    Dim removeJewels = CheckForRow()


                    makeTransparant(removeJewels)

                    If removeJewels.Count <> 0 Then
                        dropJewels()
                    End If

                End If

            Next
        Next


    End Sub

    ' gives all tiles with a transparant backcolour a new backcolour
    Private Sub changeTransparantColour()

        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0)
            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1)

                If gameBoard(i, j).BackColor = Color.Transparent Then

                    gameBoard(i, j).BackColor = give_colour()

                End If

            Next
        Next

    End Sub

    Private Sub makeTransparant(removeSet As List(Of Label))

        For i As Integer = 0 To removeSet.Count - 1

            removeSet(i).BackColor = Color.Transparent

        Next

        Me.Refresh()
    End Sub


    ' selects or deselects a label , when a label has already been clicked it deselects the label
    Private Sub select_deselectLabel(currentLabel As Label)

        ' If the jewel had already been selected it is deselected now
        If currentLabel.Tag Is "Selected" Then

            currentLabel.Tag = ""
            If checkingMoves = False Then

                labelsSelected -= 1
            End If

        Else
            currentLabel.Tag = "Selected"    ' indictates that a label is selected

            If checkingMoves = False Then
                labelsSelected += 1
            End If

            If labelsSelected > 2 Then
                currentLabel.Tag = ""

                If checkingMoves = False Then
                    labelsSelected -= 1

                End If
            End If

            End If

    End Sub

    ' converts from tile location to index of gameboard at dimension 0

    Private Function convertToIndex0(selectedLabel As Label) As Integer

        Dim i As Integer = selectedLabel.Location.X
        i = CInt(i / (TILE_HEIGHT) - 1)

        Return i

    End Function

    ' converts from tile location to index of gameboard at dimension 1

    Private Function convertToIndex1(selectedLabel As Label) As Integer

        Dim j As Integer = selectedLabel.Location.Y
        j = CInt(j / (TILE_HEIGHT) - 1)
        Return j

    End Function

    ' swaps two jewels
    Private Function swapJewels(currentLabel As Label) As List(Of Label)        ' returns list of swapped labels so the can be unswapped when they don't make a new row

        Dim swappedJewels As List(Of Label) = New List(Of Label)

        Dim i = convertToIndex0(currentLabel)
        Dim j = convertToIndex1(currentLabel)

        Dim firstJewel As Label = gameBoard(i, j)
        Dim rightJewel As Label = gameBoard(i, j)
        Dim leftJewel As Label = gameBoard(i, j)
        Dim upJewel As Label = gameBoard(i, j)
        Dim downJewel As Label = gameBoard(i, j)

        If i = 0 Then

            firstJewel = gameBoard(i, j)
            rightJewel = gameBoard(i + 1, j)
            leftJewel = gameBoard(Nothing, Nothing)
            downJewel = gameBoard(i, j + 1)
            upJewel = gameBoard(Nothing, Nothing)

            If j <> 0 Then
                upJewel = gameBoard(i, j - 1)
            End If

        ElseIf j = 0 Then
            firstJewel = gameBoard(i, j)
            rightJewel = gameBoard(i + 1, j)
            leftJewel = gameBoard(i - 1, j)
            upJewel = gameBoard(Nothing, Nothing)
            downJewel = gameBoard(i, j + 1)


        ElseIf i = MATRIX_HEIGHT - 1 Then
            firstJewel = gameBoard(i, j)
            rightJewel = gameBoard(Nothing, Nothing)
            leftJewel = gameBoard(i - 1, j)
            upJewel = gameBoard(Nothing, Nothing)
            downJewel = gameBoard(i, j + 1)

            If j <> 0 Then
                upJewel = gameBoard(i, j - 1)
            End If

        ElseIf j = MATRIX_HEIGHT - 1 Then
            firstJewel = gameBoard(i, j)
            rightJewel = gameBoard(i + 1, j)
            leftJewel = gameBoard(i - 1, j)
            upJewel = gameBoard(i, j - 1)
            downJewel = gameBoard(Nothing, Nothing)



        Else
            firstJewel = gameBoard(i, j)
            rightJewel = gameBoard(i + 1, j)
            leftJewel = gameBoard(i - 1, j)
            upJewel = gameBoard(i, j - 1)
            downJewel = gameBoard(i, j + 1)

        End If

        Dim changeColour = firstJewel.BackColor

        If rightJewel.Tag Is "Selected" Then

            firstJewel.Tag = ""
            rightJewel.Tag = ""

            If checkingMoves = False Then
                labelsSelected -= 2
            End If


            gameBoard(i, j).BackColor = rightJewel.BackColor
            gameBoard(i + 1, j).BackColor = changeColour


            swappedJewels.Add(firstJewel)
            swappedJewels.Add(rightJewel)

            remainingSteps -= 1

            If checkingMoves = False Then

                Me.Refresh()
            End If
            Return swappedJewels

        ElseIf leftJewel.Tag Is "Selected" Then

            firstJewel.Tag = ""
            leftJewel.Tag = ""

            If checkingMoves = False Then
                labelsSelected -= 2
            End If

            If i <> 0 Then

                gameBoard(i, j).BackColor = leftJewel.BackColor
                gameBoard(i - 1, j).BackColor = changeColour


            End If

            swappedJewels.Add(firstJewel)
            swappedJewels.Add(leftJewel)

            remainingSteps -= 1

            If checkingMoves = False Then

                Me.Refresh()
            End If

            Return swappedJewels

        ElseIf upJewel.Tag Is "Selected" Then

            firstJewel.Tag = ""
            upJewel.Tag = ""
            If checkingMoves = False Then
                labelsSelected -= 2
            End If

            gameBoard(i, j).BackColor = upJewel.BackColor
            gameBoard(i, j - 1).BackColor = changeColour

            swappedJewels.Add(firstJewel)
            swappedJewels.Add(upJewel)

            remainingSteps -= 1

            If checkingMoves = False Then

                Me.Refresh()
            End If

            Return swappedJewels

        ElseIf downJewel.Tag Is "Selected" Then

            firstJewel.Tag = ""
            downJewel.Tag = ""

            If checkingMoves = False Then
                labelsSelected -= 2
            End If

            gameBoard(i, j).BackColor = downJewel.BackColor
            gameBoard(i, j + 1).BackColor = changeColour

            swappedJewels.Add(firstJewel)
            swappedJewels.Add(downJewel)

            remainingSteps -= 1

            If checkingMoves = False Then

                Me.Refresh()
            End If

            Return swappedJewels

        Else
            firstJewel.Tag = ""

            If checkingMoves = False Then
                labelsSelected -= 2     ' is 2 becaus movespossible is called after this and erases the selected tag from the second Jewel
            End If

            Return Nothing
        End If

    End Function

    Private Sub unswapJewels(swappedJewels As List(Of Label))

        Dim JewelLocation1 = swappedJewels(0).Location

        Dim i = convertToIndex0(swappedJewels(0))
        Dim j = convertToIndex1(swappedJewels(0))

        Dim k = convertToIndex0(swappedJewels(1))
        Dim l = convertToIndex1(swappedJewels(1))

        Dim firstColour = gameBoard(i, j).BackColor

        gameBoard(i, j).BackColor = gameBoard(k, l).BackColor
        gameBoard(k, l).BackColor = firstColour

        remainingSteps += 1

    End Sub

    ' Geeft een rand aan de tegels
    Sub paintborder(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint

        Dim g As Graphics = e.Graphics
        Dim pen As New Pen(Color.White, 5.0)

        For Each ctr As Control In Me.Controls
            If TypeOf ctr Is Label Then

                Dim currentLabel As Label = CType(ctr, Label)

                If currentLabel.Tag Is "Selected" Then
                    g.DrawRectangle(pen, New Rectangle(currentLabel.Location, currentLabel.Size))

                End If
            End If

        Next
        pen.Dispose()

    End Sub

    Private Sub checkNmbrSteps()

        If remainingSteps = 0 Then
            My.Computer.Audio.Play(My.Resources.completed, AudioPlayMode.WaitToComplete)
            MsgBox(" You have used all your moves, your score is " + CType(score, String))

            If score > highscore Then

                MsgBox("New Highscore! Congratulations you beat the previous score of " + CType(highscore, String))
                highscore = score
            End If

        End If

    End Sub

    ' Function checks if there are more moves possible and starts a new game when it isn't possible
    ' the player score stays the same
    Private Sub movesPossible()

        checkingMoves = True

        Dim possibleMoves As Integer = 0

        For i As Integer = gameBoard.GetLowerBound(0) To gameBoard.GetUpperBound(0) - 1
            For j As Integer = gameBoard.GetLowerBound(1) To gameBoard.GetUpperBound(1) - 1

                Dim currentJewel = gameBoard(i, j)
                Dim rightJewel = gameBoard(i, j + 1)
                Dim downJewel = gameBoard(i + 1, j)
                Dim n As Integer = 0

                rightJewel.Tag = "Selected"         ' to check if jewel forms a new combination when swapped with the right jewel or the downJewel

                While n < 3

                    Dim swappedJewels = swapJewels(currentJewel)
                    Dim removeJewels = CheckForRow()

                    If removeJewels.Count <> 0 Then
                        possibleMoves += 1
                    End If

                    If swappedJewels IsNot Nothing Then

                        unswapJewels(swappedJewels)
                    End If

                    rightJewel.Tag = ""
                    downJewel.Tag = "Selected"

                    n += 1

                End While

                downJewel.Tag = ""

            Next
        Next

        checkingMoves = False

        If possibleMoves = 0 Then
            MsgBox("No possible moves left")

            emptyGameboard()
            make_gameBoard()

        End If
    End Sub

    ' sets the value in gameboard to the value of the selected label
    Private Sub update_gameboard(currentLabel As Label)
        Dim i = convertToIndex0(currentLabel)

        Dim j = convertToIndex1(currentLabel)

        gameBoard(i, j) = currentLabel
    End Sub


    ' closes form when quit is selected in menu
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitGAmeMenu.Click
        Close()
    End Sub


    Private Sub OnToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OnToolStripMenuItem.Click
        cheatmode = True
    End Sub

    Private Sub OffToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OffToolStripMenuItem.Click
        cheatmode = False
    End Sub

End Class

