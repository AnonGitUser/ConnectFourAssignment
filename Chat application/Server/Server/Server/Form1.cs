using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public partial class Server : Form
    {
        delegate void AppendTextDelegate(string s);
        AppendTextDelegate textAppender;
        Socket serverSocket;
        IPAddress thisAddress;
        List<Socket> connectClientList;

        public Server()
        {
            InitializeComponent();
        }

        private void Server_Load(object sender, EventArgs e)
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            textAppender = new AppendTextDelegate(AppendText);
            connectClientList = new List<Socket>();

            //gets local ip address
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress addr in hostEntry.AddressList)
            {
                if (addr.AddressFamily == AddressFamily.InterNetwork)
                {
                    thisAddress = addr;
                    break;
                }
            }
            
            //assigns local ip to the text box
            if (thisAddress == null) thisAddress = IPAddress.Loopback;
            tbIP.Text = thisAddress.ToString();
            dgvUsernames.ReadOnly = true;
        }


        //test for abilitiy to startup server
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            int port;
            if (serverSocket == null)
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                connectClientList = new List<Socket>();
            }
            try { port = Int32.Parse(tbPort.Text); }
            catch
            {
                //error if no port number or port number incorrect
                MessageBox.Show("Port number entered incorrectly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPort.Focus();
                tbPort.SelectAll();
                return;
            }

            //error if you click start server when it is already running
            if (serverSocket.IsBound)
                MessageBox.Show("The server is running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (port < 0 || port > 65535)
            {
                //port number error if it is not within parameters of port numbers
                MessageBox.Show("Port number entered incorrectly.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                tbPort.Focus();
                tbPort.SelectAll();
            }
            else
            {
                //starts up server
                IPEndPoint endPoint = new IPEndPoint(thisAddress, port);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(20);

                serverSocket.BeginAccept(AcceptCallback, null);
                AppendText("Server startup completed.");
            }
        }

        //Client fires when a connection signal comes in Callback
        private void AcceptCallback(IAsyncResult asyncResult)
        {
            try
            {
                Socket client = serverSocket.EndAccept(asyncResult);
                serverSocket.BeginAccept(AcceptCallback, null);

                AsyncObject asyncObject = new AsyncObject(4096);
                asyncObject.WorkingSocket = client;
                connectClientList.Add(client);

                AppendText("IP: " + client.RemoteEndPoint);
                client.BeginReceive(asyncObject.Buffer, 0, 4096, 0, ReceiveData, asyncObject);
            }
            catch { }
        }

        //Data start when you recieve Callback
        private void ReceiveData(IAsyncResult asyncResult)
        {
            AsyncObject asyncObject = asyncResult.AsyncState as AsyncObject;
            try { asyncObject.WorkingSocket.EndReceive(asyncResult); }
            catch
            {
                asyncObject.WorkingSocket.Close();
                return;
            }

            string text = Encoding.UTF8.GetString(asyncObject.Buffer);
            string[] tokens = text.Split('\x01');
            try
            {
                if (tokens[1][0] == '\x02')
                {
                    //shows user joining and displays number of users in server
                    AppendText(tokens[0] + " has joined (current people: " + connectClientList.Count + " people)");
                    try { dgvUsernames.Rows.Add(new string[] { tokens[0] }); }
                    catch { }
                }
                else if (tokens[1][0] == '\x03')
                {
                    //shows user leaving and displays number of users in server
                    AppendText(tokens[0] + " has left (current people: " + (connectClientList.Count - 1) + " people)");
                    try
                    {
                        for (int i = 0; i < dgvUsernames.Rows.Count; i++)
                        {
                            if (tokens[0] == dgvUsernames.Rows[i].Cells[0].Value as string)
                            {
                                dgvUsernames.Rows.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    catch { }
                }
                //message recieved from client
                else AppendText("[Recieved] " + tokens[0] + ": " + tokens[1]);
            }
            catch { }
            for (int i = connectClientList.Count - 1; i >= 0; i--)
            {
                Socket tempSocket = connectClientList[i];
                if (tempSocket != asyncObject.WorkingSocket)
                {
                    try { tempSocket.Send(asyncObject.Buffer); }
                    catch
                    {
                        tempSocket.Close();
                        connectClientList.RemoveAt(i);
                    }
                }
            }

            asyncObject.ClearBuffer();
            try { asyncObject.WorkingSocket.BeginReceive(asyncObject.Buffer, 0, 4096, 0, ReceiveData, asyncObject); }
            catch
            {
                asyncObject.WorkingSocket.Close();
                connectClientList.Remove(asyncObject.WorkingSocket);
            }
        }

        private void SendText(string message)
        {
            if (!serverSocket.IsBound) MessageBox.Show("Server is not running.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("No text entered.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbMessage.Focus();
            }
            else
            {
                SendProcess(Encoding.UTF8.GetBytes("manager\x01" + message));
                AppendText("[sent] Administrator: " + message);
                tbMessage.Clear();
            }
        }

        //Send text to each client
        private void SendProcess(byte[] byteData)
        {
            for (int i = connectClientList.Count - 1; i >= 0; i--)
            {
                Socket tempSocket = connectClientList[i];
                try { tempSocket.Send(byteData); }
                catch
                {
                    tempSocket.Close();
                    connectClientList.RemoveAt(i);
                }
            }
        }

        
        //sends message from within the textbox
        private void BtnSend_Click(object sender, EventArgs e)
        {
            SendText(tbMessage.Text.Trim());
        }


        //allows message to be sent from enter key press
        private void BtnSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendText(tbMessage.Text.Trim());
        }
        private void TbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendText(tbMessage.Text.Trim());
        }


        //Terminate connection
        private void Disconnect()
        {
            if (serverSocket != null && serverSocket.IsBound)
            {
                SendProcess(Encoding.UTF8.GetBytes("manager\x01\x04"));
                serverSocket.Close();
                serverSocket = null;

                AppendText("Server shutdown is complete.");
                while (dgvUsernames.Rows.Count > 0) dgvUsernames.Rows.RemoveAt(0);
            }
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Server_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }

        //Write history such as messages, status
        private void AppendText(string message)
        {
            if (tbChat.InvokeRequired) tbChat.Invoke(textAppender, message);
            else tbChat.Text += "\r\n" + message;
        }

       
    }

    //Callback to store content for Class
    public class AsyncObject
    {
        public byte[] Buffer;
        public Socket WorkingSocket;
        public readonly int BufferSize;

        public AsyncObject(int bufferSize)
        {
            BufferSize = bufferSize;
            Buffer = new byte[BufferSize];
        }

        public AsyncObject(int buffersize, Socket tempSocket)
            : this(buffersize) { WorkingSocket = tempSocket; }

        public void ClearBuffer() { Array.Clear(Buffer, 0, BufferSize); }
    }
}
