using System;

namespace bl4ckb0t.Util
{
	public class NoPrefixUri : Uri
	{
		public NoPrefixUri(String uri) : base(uri.StartsWith("file:") ? uri.Replace("file:", "") : uri) {}
	}
}
