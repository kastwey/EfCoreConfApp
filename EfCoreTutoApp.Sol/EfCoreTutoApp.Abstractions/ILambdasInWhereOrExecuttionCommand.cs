using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreTutoApp.Abstractions
{
	public interface ILambdasInWhereOrExecuttionCommand : IDbCommand
	{

		bool UseLambdaInWhere { get; set; }

	}
}
