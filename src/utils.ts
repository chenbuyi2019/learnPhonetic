
type HTMLInputs = HTMLInputElement | HTMLSelectElement | HTMLTextAreaElement
type HTMLCanDisabledInputs = HTMLButtonElement | HTMLInputs

/** 
 * 使元素 disabled ，然后过一会恢复工作。如果时间为1以下，则永远不会自动恢复
 **/
function makeButtonCooldown(e: HTMLCanDisabledInputs, timeoutMs: number = 1000) {
    e.disabled = true
    if (timeoutMs < 1) { return }
    setTimeout(() => {
        if (e == null) { return }
        e.disabled = false
    }, timeoutMs);
}

/** 
 * 使元素 disabled ，然后过一会恢复工作。如果时间为1以下，则永远不会自动恢复
 **/
function makeButtonsCooldown(array: Array<HTMLCanDisabledInputs>, timeoutMs: number = 1000) {
    for (const e of array) {
        makeButtonCooldown(e, timeoutMs)
    }
}

/** 
 * 生成指定范围内的随机整数（包含最小值和最大值）
 **/
function getRandomInt(min: number, max: number): number {
    if (min > max) {
        throw new Error('最小值不能大于最大值');
    }
    return Math.floor(Math.random() * (max - min + 1)) + min;
}