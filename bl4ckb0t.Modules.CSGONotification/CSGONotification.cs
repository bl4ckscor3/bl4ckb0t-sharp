using bl4ckb0t.ModuleAPI;
using bl4ckb0t.Util;
using ChatSharp.Events;
using PushbulletSharp;
using PushbulletSharp.Models.Requests;

namespace bl4ckb0t.Modules.CSGONotification
{
	public class CSGONotification : BaseModule
	{
		public CSGONotification(string _name) : base(_name) { }

		public override void OnEnable(Bot client)
		{
			client.ChannelMessageRecieved += Handle;
		}

		public override void OnDisable(Bot client)
		{
			client.ChannelMessageRecieved -= Handle;
		}

		/// <summary>
		/// Handles a CS:GO update being found by Vauff's bot Maunz
		/// </summary>
		/// <param name="c">The IrcClient of the bot</param>
		/// <param name="e">The arguments of the event calling this method</param>
		public void Handle(object c, PrivateMessageEventArgs e)
		{
			string msg = e.PrivateMessage.Message;

			if(e.PrivateMessage.User.Nick.Equals("Maunz") && msg.ToLower().Contains("was pushed to the steam client!"))
			{
				Utilities.SendMessage(e.PrivateMessage.Source, "bl4ckscor3, Vauff, ^");
				Push(msg.ToLower().Contains("beta"));
			}
		}

		public override string[] Usage()
		{
			return new string[] { Resources.usage };
		}

		/// <summary>
		/// Pushes a Pushbullet notification
		/// </summary>
		/// <param name="beta">true if the update is a beta update, false otherwise</param>
		public void Push(bool beta)
		{
			PushbulletClient client = new PushbulletClient(Passwords.pushbulletapikey);

			client.PushNote(new PushNoteRequest() {
				DeviceIden = client.CurrentUsersDevices().Devices.Find(o => o.Nickname == "Phone").Iden,
				Title = "New CS:GO update!",
				Body = beta ? "Beta" : "Release"
			});
		}
	}
}
