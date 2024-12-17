// Create connection
const connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build()

// Connection method that hub invokes aka receive notifications from hub
connectionUserCount.on("updateTotalViews", (value) => {
	const newCountSpan = document.getElementById("totalViewsCounter")
	newCountSpan.innerText = value.toString()
})

connectionUserCount.on("updateTotalUsers", value => {
	const newCountSpan = document.getElementById("totalUsersCounter")
	newCountSpan.innerText = value.toString()
})

// Invoke hub methods aka send notifications to hub
function newWindowLoadClient() {
	connectionUserCount.send("NewWindowLoaded")
}

// Start connection
function fulfilled() {
	// do something on start
	console.log("Connection to User hub successful")
	newWindowLoadClient();
}
function rejected() {
	// rejected logs
	console.error("Connection to User hub fail")
}

connectionUserCount.start().then(fulfilled, rejected)