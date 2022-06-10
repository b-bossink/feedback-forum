using System;
namespace Logic
{
	public class CommunicationResult
	{
		public readonly string message;
		public readonly int code;
		public CommunicationResult(string _message, int _code)
		{
			message = _message;
			code = _code;
		}

		// 0 - 99
		public readonly static CommunicationResult Succes =
			new CommunicationResult(null, 0);

		public readonly static CommunicationResult UnexpectedError =
			new CommunicationResult("Something unexpected happened", 1);

		public readonly static CommunicationResult PostNotFoundError =
			new CommunicationResult("The post you are trying to access doesn't exist.", 2);

		// 100 - 199
		public readonly static CommunicationResult DuplicateEmailError =
			new CommunicationResult("The emailadress already exists.", 100);

		public readonly static CommunicationResult DuplicateUsernameError =
			new CommunicationResult("The username already exists.", 101);

		// 200 - 299
		public readonly static CommunicationResult NoCommunicationError =
			new CommunicationResult("Failed to connect.", 200);
	}
}

