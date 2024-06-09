import type { Metadata } from "next";
import { CertificateMapping } from "../../../web-api-client";

type Props = {
  params: { id: string };
};

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const id = params.id;

  const data: CertificateMapping = await fetch(
    `${process.env.WEBSITE_URL}/api/subject/${id}`
  ).then((res) => res.json());

  return {
    title: data?.name,
  };
}
export default function DashboardCertificateLayout({ children }: { children: React.ReactNode }) {
  return children;
}
