import ChatView from "features/chat-view/ui"
import { ChatProvider } from "shared/api"

function ChatPage() {
	return (
		<div className="Chat">
			<h1 className="center-text">Chat</h1>

			<ChatProvider>
				<ChatView />
			</ChatProvider>
		</div>
	)
}

export default ChatPage
