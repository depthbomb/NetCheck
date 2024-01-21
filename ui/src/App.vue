<script setup lang="ts">
	import { ref, onMounted } from 'vue';
	import WifiIcon from './components/WifiIcon.vue';
	import { useFetch, useInterval } from '@vueuse/core';
	import EthernetIcon from './components/EthernetIcon.vue';
	import { HubConnectionBuilder } from '@microsoft/signalr';

	const online                                      = ref(false);
	const latency                                     = ref(0);
	const potentiallyDegraded                         = ref(false);
	const usingEthernet                               = ref(false);
	const { counter, reset, pause, resume, isActive } = useInterval(1_000, { controls: true });
	const connection                                  = new HubConnectionBuilder().withUrl('/connection').build();

	connection.on('Online', (val: boolean) => online.value = val);
	connection.on('Latency', (val: number) => latency.value = val);

	onMounted(async () => {
		const { error, data } = await useFetch('/network_type');
		if (!error.value) {
			usingEthernet.value = data.value === 'ethernet';
		}

		await connection.start();

		setInterval(() => {
			if (latency.value >= 100) {
				if (!isActive.value) {
					resume();
				}
			} else {
				reset();
				pause();
			}

			potentiallyDegraded.value = counter.value >= 90;
		}, 1_000);
	});
</script>

<template>
	<main
		class="flex w-screen h-screen items-center justify-center"
		:class="{ 'bg-[#49cb6d]': online, '!bg-[#db192b]': !online, 'bg-[#f7630c]': potentiallyDegraded }"
	>
		<div class="flex flex-col items-center justify-center text-[calc(8vh_+_8vw)] text-white select-none">
			<h1 class="font-bold leading-none">{{ online ? 'Online' : 'Offline' }}</h1>
			<p class="flex flex-row space-x-2 items-center text-[calc(2vh_+_2vw)]">
				<component :is="usingEthernet ? EthernetIcon : WifiIcon" class="w-[3vw]"/>
				<span class="font-mono">{{ online ? Math.round(latency) : '???' }}ms</span>
			</p>
			<span v-if="potentiallyDegraded && online" class="text-[calc(1vh_+_1vw)]">Average latency has been at 100ms+ for a prolonged period of time. Network performance may be degraded.</span>
		</div>
	</main>
</template>
