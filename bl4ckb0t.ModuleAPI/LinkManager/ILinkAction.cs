namespace bl4ckb0t.ModuleAPI
{
	public interface ILinkAction
	{
		/// <summary>
		/// Handles the sent link
		/// </summary>
		/// <param name="channel">The channel the link got sent in</param>
		/// <param name="user">The user that sent the link</param>
		/// <param name="link">The link itself</param>
		void Handle(string channel, string user, string link);

		/// <summary>
		/// Checks wether or not the given link is valid for this LinkAction
		/// </summary>
		/// <param name="channel">The channel the link got sent in</param>
		/// <param name="link">The link to check</param>
		/// <returns>true if the link can be executed by this LinkAction, false otherwise</returns>
		bool IsValid(string link);

		/// <summary>
		/// The priority with which the link should get handled. The higher the priority, the earlier the link will be handled
		/// </summary>
		/// <returns></returns>
		int Priority();
	}
}
