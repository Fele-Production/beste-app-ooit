import discogs_client

d = discogs_client.Client('ExampleApplication/0.1', user_token="SuycForINtHDBKeVxxSGLQSsOtkjOeGpCdKBzENj")
results = d.search('The Tortured Poets Department', type='release')
artist = results[0].artists[0]
print(artist.name)
