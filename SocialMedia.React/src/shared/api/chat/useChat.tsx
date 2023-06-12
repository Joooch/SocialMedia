import {
    createContext,
    ReactNode,
    useCallback,
    useContext,
    useEffect,
    useMemo,
    useState,
} from "react"
import { useCookies } from "react-cookie"
import { Chat } from "./lib"

interface ChatContextType {
	chat: Chat | null
	loading: boolean
}

const ChatContext = createContext<ChatContextType>({} as ChatContextType)

export function ChatProvider({
	children,
}: {
	children: ReactNode
}): JSX.Element {
	const [chat, setChat] = useState<Chat | null>(null)
	const [loading, setLoading] = useState<boolean>(true)
	const [cookies] = useCookies(["Token"])

	const login = useCallback(() => {
		let jwtToken = cookies.Token
		if (!jwtToken) {
			setLoading(false)
			return
		}

		setLoading(true)
		const newChat = new Chat()
		newChat.login(jwtToken).then((connected) => {
			setLoading(false)
		})

		setChat(newChat)
	}, [cookies.Token])

	useEffect(() => {
		login()

		return () => {
			chat?.close()
		}
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [cookies.Token])

	const memoedValue = useMemo(
		() => ({
			chat,
			loading,
		}),
		[chat, loading]
	)

	return (
		<ChatContext.Provider value={memoedValue}>
			{children}
		</ChatContext.Provider>
	)
}

export function useChat() {
	return useContext(ChatContext)
}
