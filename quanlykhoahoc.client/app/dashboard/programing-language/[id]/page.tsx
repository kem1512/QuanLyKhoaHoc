"use client";

import ProgramingLanguageHandler from "../../../../components/Handler/ProgramingLanguageHandler";

export default function DashboardProgramingLanguageUpdate({
  params,
}: {
  params: { id: number };
}) {
  const { id } = params;

  return <ProgramingLanguageHandler id={id} />;
}
