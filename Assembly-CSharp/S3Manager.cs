using System;
using System.IO;
using System.Net;
using System.Threading;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using RSG;
using SAES;
using UnityEngine;

// Token: 0x02000591 RID: 1425
public class S3Manager : MonoBehaviour
{
	// Token: 0x17000282 RID: 642
	// (get) Token: 0x06002F5A RID: 12122 RVA: 0x0016D92C File Offset: 0x0016BB2C
	private IAmazonS3 _client
	{
		get
		{
			if (this._s3_client == null)
			{
				try
				{
					SAES saesClient = new SAES();
					this._abnn = saesClient.ToString("Js23DGKu7RMNik4XECoQVkNFIW//dsZNcfyKb49RlFU");
					this._s3_client = new AmazonS3Client(saesClient.ToString("VvrCEe1TcUBvQeiSelndpl1Plc4FoMxddSglHA2Fe0M"), saesClient.ToString("WVbbIlYTAH37Glxvl1MSDpKPffhczwdbi5FRgkSs8mkLEuLzE6YCiouHH71vVgLS"), RegionEndpoint.USEast2);
					saesClient.init(false);
				}
				catch (Exception ex)
				{
					Debug.LogError("s3 manager not working");
					Debug.LogError(ex.Message);
				}
			}
			return this._s3_client;
		}
	}

	// Token: 0x06002F5B RID: 12123 RVA: 0x0016D9B4 File Offset: 0x0016BBB4
	private void Start()
	{
		S3Manager.instance = this;
		Config.upload_available = false;
	}

	// Token: 0x06002F5C RID: 12124 RVA: 0x0016D9C4 File Offset: 0x0016BBC4
	public Promise<string> uploadFileToAWS3(string pFileName, byte[] pFileRawBytes)
	{
		Promise<string> tPromise = new Promise<string>();
		try
		{
			MemoryStream tStream = new MemoryStream(pFileRawBytes);
			PutObjectRequest tRequest = new PutObjectRequest
			{
				BucketName = this._abnn,
				Key = pFileName,
				InputStream = tStream,
				CannedACL = S3CannedACL.Private
			};
			if (this._client.PutObjectAsync(tRequest, default(CancellationToken)).WaitAndUnwrapException<PutObjectResponse>().HttpStatusCode == HttpStatusCode.OK)
			{
				tPromise.Resolve(tRequest.Key);
			}
			else
			{
				tPromise.Reject(new Exception("Error when uploading!"));
			}
		}
		catch (WebException ex)
		{
			using (Stream stream = ex.Response.GetResponseStream())
			{
				using (StreamReader reader = new StreamReader(stream))
				{
					Debug.Log(reader.ReadToEnd());
				}
			}
			tPromise.Reject(ex);
		}
		catch (Exception ex2)
		{
			tPromise.Reject(ex2);
		}
		return tPromise;
	}

	// Token: 0x04002392 RID: 9106
	public static S3Manager instance;

	// Token: 0x04002393 RID: 9107
	private const string ABN = "Js23DGKu7RMNik4XECoQVkNFIW//dsZNcfyKb49RlFU";

	// Token: 0x04002394 RID: 9108
	private const string AAK = "VvrCEe1TcUBvQeiSelndpl1Plc4FoMxddSglHA2Fe0M";

	// Token: 0x04002395 RID: 9109
	private const string ASK = "WVbbIlYTAH37Glxvl1MSDpKPffhczwdbi5FRgkSs8mkLEuLzE6YCiouHH71vVgLS";

	// Token: 0x04002396 RID: 9110
	private string _abnn;

	// Token: 0x04002397 RID: 9111
	private IAmazonS3 _s3_client;
}
