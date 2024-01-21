import { resolve } from 'node:path';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import { exec } from 'node:child_process';

const outDir = resolve(__dirname, '..', 'NetCheck.Worker', 'static');

export default defineConfig({
	json: {
		stringify: true,
	},
	build: {
		outDir,
		minify: 'esbuild',
		emptyOutDir: true,
		assetsInlineLimit: 0,
		rollupOptions: {
			output: {
				entryFileNames: '[hash:22].js',
				chunkFileNames: '[hash:22].js',
				assetFileNames: '[hash:22].[ext]',
			},
		},
	},
	plugins: [
		vue(),
		{
			name: 'create-zip',
			enforce: 'post',
			transform() {
				exec(`zip -r ./ui.zip ${outDir}`)
			}
		}
	],
});
