/** @type {import('next').NextConfig} */
const nextConfig = {
  reactStrictMode: false,
  swcMinify: true,
  async rewrites() {
    return [
      {
        source: "/api/:path*",
        destination: `${process.env.WEBSITE_URL}/api/:path*`,
      },
      {
        source: "/images/:path*",
        destination: `${process.env.WEBSITE_URL}/images/:path*`,
      },
    ];
  },
};

export default nextConfig;
