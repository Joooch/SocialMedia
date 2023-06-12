import { Stack } from "@mui/material"
import { useEffect, useState } from "react"
import { useChat } from "shared/api"
import ChatFriendsList from "./friends-list"
import "./index.css"
import ChatMessagesView from "./messages-view"

export default function ChatView() {
	const [chatId, setChatId] = useState<string | undefined>()
	const { chat } = useChat()

	useEffect(() => {}, [])

	const handleClick = () => {
		console.log("handle click")
	}

	return (
		<div className="chats-view">
			<Stack direction="row" spacing={5}>
				<ChatFriendsList
					selectedChat={chatId}
					onFriendSelected={(id) => setChatId(id)}
				/>
				{chatId && <ChatMessagesView targetId={chatId} />}
			</Stack>
		</div>
	)
}
