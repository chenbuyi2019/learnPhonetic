/// <reference path="utils.ts" />

interface WordDictData {
    Word: string
    Phonetic: string
    Translation: string
    Tags: string[]
}

const allWords: WordDictData[] = []

function AddWord(word: string, phonetic: string, tags: string[], trans: string) {
    word = word.toLowerCase().trim()
    let v: WordDictData = {
        Word: word,
        Translation: trans,
        Phonetic: phonetic,
        Tags: tags
    }
    allWords.push(v)
}
ImportWordsData()
console.log("导入单词完成", allWords.length)

const selWordGroup = document.getElementById("selWordGroup") as HTMLSelectElement
const divQuestion = document.getElementById("divQuestion") as HTMLDivElement
const divAnswer = document.getElementById("divAnswer") as HTMLDivElement
const divAnswerText = document.getElementById("divAnswerText") as HTMLElement
const divResult = document.getElementById("divResult") as HTMLDivElement
const iframeWeb = document.getElementById("iframeWeb") as HTMLIFrameElement
const btnNewQuestion = document.getElementById("btnNewQuestion") as HTMLButtonElement
const btnCheckAnswer = document.getElementById("btnCheckAnswer") as HTMLButtonElement
const txtInputAnswer = document.getElementById("txtInputAnswer") as HTMLInputElement

const buttons = [btnNewQuestion, btnCheckAnswer]

const allGroups = new Map<string, WordDictData[]>()

for (const opt of selWordGroup.options) {
    const groupName = opt.value
    let group: WordDictData[] = []
    for (const w of allWords) {
        if (w.Tags.includes(groupName)) {
            group.push(w)
        }
    }
    allGroups.set(groupName, group)
    opt.innerText += ` (${group.length})`
}

let lastWord: WordDictData | null = null

btnNewQuestion.addEventListener("click", function () {
    makeButtonsCooldown(buttons, 500)

    divResult.innerText = ""
    let group = selWordGroup.value
    let words = allGroups.get(group)
    if (words == null || words.length < 1) { return }

    let index = getRandomInt(0, words.length - 1)
    let word = words[index]!
    lastWord = word

    txtInputAnswer.disabled = false
    divQuestion.innerText = `/${word.Phonetic}/`
    txtInputAnswer.value = ""
    divAnswer.style.display = "none"
    divAnswerText.innerText = `${word.Word}\n/${word.Phonetic}/\n${word.Translation}`.trim()
})

function CheckAnswer() {
    if (lastWord == null) { return }
    let answer = txtInputAnswer.value.trim().toLowerCase()
    if (answer.length < 1) { return }
    txtInputAnswer.disabled = true
    makeButtonsCooldown(buttons, 500)
    if (answer == lastWord.Word) {
        divResult.innerText = "正确"
        divResult.style.color = "green"
    } else {
        divResult.innerText = "错误"
        divResult.style.color = "red"
    }
    divAnswer.style.display = "block"
    iframeWeb.src = `https://m.youdao.com/m/result?word=${lastWord.Word}&lang=en`
}

btnCheckAnswer.addEventListener("click", CheckAnswer)

selWordGroup.disabled = false
btnNewQuestion.disabled = false
