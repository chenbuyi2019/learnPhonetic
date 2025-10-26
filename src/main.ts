

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
selWordGroup.disabled = false

for (const opt of selWordGroup.options) {
    let count = 0
    for (const w of allWords) {
        if (w.Tags.includes(opt.value)) {
            count += 1
        }
    }
    opt.innerText += ` (${count})`
}